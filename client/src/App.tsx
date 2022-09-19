import React from "react";
import API from "./api";
import { Header } from "./components";
import AppContext from "./context";
import {
  AllRequestsView,
  CreateRequestView,
  MyRequestsApproveView,
  MyRequestsView,
} from "./pages";
import "./App.css";

declare global {
  interface Window {
    getCurrentUser(func: (userData: string[]) => void): void;
  }
}

const App: React.FC = () => {
  const [selectedView, setSelectedView] = React.useState<number>(0);
  const [currentUser, setCurrentUser] = React.useState<string[]>([]);
  const [allowedPages, setAllowedPages] = React.useState<string[]>([]);

  React.useEffect(() => {
    async function fetch() {
      try {
        await window.getCurrentUser((user) => {
          API.requests
            .getLdapUser(user[0])
            .then((res) => setCurrentUser([...user, res.id]));
        });
      } catch {
        setCurrentUser([
          "test@osinit.com",
          "Тестов Тест Тестович",
          "6e2bedf3-4d98-429d-9143-6b166e845b98",
        ]);
      }
    }
    fetch();
  }, []);

  React.useEffect(() => {
    try {
      API.requests.getTabs(currentUser[2]).then((res) => setAllowedPages(res));
    } catch {
      setAllowedPages(["My requests", "New request", "I approve"]);
    }
  }, [currentUser]);

  const renderView = (viewIndex: number) => {
    switch (viewIndex) {
      case 0:
        return <CreateRequestView />;
      case 1:
        return <MyRequestsView />;
      case 2:
        return <MyRequestsApproveView />;
      case 3:
        return <AllRequestsView />;
      default:
        throw new Error();
    }
  };

  return (
    <AppContext.Provider value={currentUser}>
      <div className="App">
        <Header
          title="Заявки"
          selectedView={selectedView}
          onSelectedViewChanged={setSelectedView}
          allowedPages={allowedPages}
        />
        {renderView(selectedView)}
      </div>
    </AppContext.Provider>
  );
};

export default App;
