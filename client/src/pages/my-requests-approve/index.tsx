/* eslint-disable no-nested-ternary */
import React from "react";
import { Table } from "antd";
import Column from "antd/lib/table/Column";
import API from "../../api";
import AppContext from "../../context";
import { RequestForApprove } from "../../types/types";
import ViewRequest from "../view-request";
import { CheckApprove } from "../../components";

interface DataType {
  key: React.Key;
  requestId: number;
  requestAuthor: string;
  requestName: string;
  isApproved?: boolean;
}

const MyRequestsApproveView: React.FC = () => {
  const currentUser = React.useContext(AppContext);
  const [requests, setRequests] = React.useState<RequestForApprove[]>([]);
  const [requestId, setRequestId] = React.useState<number>(-1);

  React.useEffect(() => {
    API.requests
      .getRequests(currentUser[2])
      .then((data) => setRequests(data.requestForApprove));
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
        actions="approver"
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
      <Column title="Автор" dataIndex="authorName" key="requestAuthor" />
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

export default MyRequestsApproveView;
