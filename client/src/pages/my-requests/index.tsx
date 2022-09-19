/* eslint-disable no-nested-ternary */
import React from "react";
import { Table } from "antd";
import Column from "antd/lib/table/Column";
import API from "../../api";
import AppContext from "../../context";
import { MyRequest } from "../../types/types";
import ViewRequest from "../view-request";
import { CheckApprove } from "../../components";

interface DataType {
  key: React.Key;
  requestId: number;
  requestName: string;
  isApproved?: boolean;
}

const MyRequestsView: React.FC = () => {
  const currentUser = React.useContext(AppContext);
  const [requests, setRequests] = React.useState<MyRequest[]>([]);
  const [requestId, setRequestId] = React.useState<number>(-1);

  const fetchData = (): Promise<void> =>
    API.requests
      .getRequests(currentUser[2])
      .then((data) => setRequests(data.requests));

  React.useEffect(() => {
    fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const onRequestSelected = (row: MyRequest) => {
    setRequestId(row.id);
  };

  const onActionClick = (
    action: "cancel" | "approve" | "decline" | "recall",
  ) => {
    switch (action) {
      case "cancel":
        fetchData();
        setRequestId(-1);
        break;
      default:
        break;
    }
  };

  if (requestId !== -1) {
    return (
      <ViewRequest
        requestId={requestId}
        actions="owner"
        onActionClick={onActionClick}
      />
    );
  }

  return (
    <Table
      rowKey="id"
      dataSource={requests}
      pagination={{ position: ["bottomCenter"], pageSize: 20 }}
      onRow={(record) => ({
        onClick: () => onRequestSelected(record),
      })}
    >
      <Column title="Номер заявки" dataIndex="id" key="requestId" />
      <Column title="Тип заявки" dataIndex="name" key="requestName" />
      <Column
        title="Статус заявки"
        key="requestApproved"
        dataIndex="isApproved"
        render={(_: unknown, record: DataType) => (
          <CheckApprove isApproved={record.isApproved} />
        )}
      />
    </Table>
  );
};

export default MyRequestsView;
