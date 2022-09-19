/* eslint-disable no-nested-ternary */
import { Table, Typography } from "antd";
import Column from "antd/lib/table/Column";
import React from "react";
import API from "../../api";
import AppContext from "../../context";
import { RequestForApprove } from "../../types";
import ViewRequest from "../view-request";

interface DataType {
  key: React.Key;
  requestId: number;
  requestAuthor: string;
  requestName: string;
  isApproved: string;
}

const AllRequestsView: React.FC = () => {
  const currentUser = React.useContext(AppContext);
  const [allRequests, setRequests] = React.useState<RequestForApprove[]>([]);
  const [requestId, setRequestId] = React.useState<number>(-1);

  React.useEffect(() => {
    API.requests
      .getRequests(currentUser[2])
      .then((data) => setRequests(data.allRequests));
  }, [currentUser]);

  const onRequestSelected = (row: RequestForApprove) => {
    setRequestId(row.id);
  };

  const onActionClick = (
    action: "cancel" | "approve" | "decline" | "recall",
  ) => {
    switch (action) {
      case "cancel":
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
        actions="viewer"
        onActionClick={onActionClick}
      />
    );
  }

  if (allRequests.length !== 0) {
    return (
      <Table
        rowKey="id"
        dataSource={allRequests}
        pagination={{ position: ["bottomCenter"], pageSize: 20 }}
        onRow={(record) => ({
          onClick: () => onRequestSelected(record),
        })}
      >
        <Column title="Номер заявки" dataIndex="id" key="requestId" />
        <Column title="Тип заявки" dataIndex="name" key="requestName" />
        <Column title="Автор" dataIndex="authorName" key="requestAuthor" />
        <Column
          title="Статус заявки"
          key="requestApproved"
          dataIndex="isApproved"
          render={(_: unknown, record: DataType) => (
            <Typography.Text>
              {record.isApproved === null
                ? "Ожидает согласования"
                : record.isApproved
                ? "Согласована"
                : "Не согласована"}
            </Typography.Text>
          )}
        />
      </Table>
    );
  }
  return null;
};

export default AllRequestsView;
