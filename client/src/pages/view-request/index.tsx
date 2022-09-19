/* eslint-disable no-nested-ternary */
import { Button, Divider, notification, Space, Table, Typography } from "antd";
import Column from "antd/lib/table/Column";
import React from "react";
import API from "../../api";
import { CheckApprove } from "../../components";
import AppContext from "../../context";
import {
  RequestData,
  RequestDataControl,
  RequestDetailsType,
} from "../../types";

interface ApproverDataType {
  key: React.Key;
  userName: number;
  isApproved?: boolean;
}

type ViewRequestProps = {
  requestId: number;
  actions: "approver" | "owner" | "viewer";
  onActionClick: (action: "cancel" | "approve" | "decline" | "recall") => void;
} & React.HTMLAttributes<HTMLDivElement>;

const ViewRequest: React.FC<ViewRequestProps> = ({
  requestId,
  actions,
  onActionClick,
}) => {
  const currentUser = React.useContext(AppContext);
  const [requestDetails, setRequestDetails] =
    React.useState<RequestDetailsType>();
  const [requestData, setRequestData] = React.useState<RequestData>();

  React.useEffect(() => {
    API.requests.getRequest(requestId, currentUser[2]).then((res) => {
      setRequestDetails(res);
      setRequestData(JSON.parse(res.requestData) as RequestData);
    });
  }, [currentUser, requestId]);

  const openNotification = (description: string): void => {
    notification.open({
      message: "Успех",
      description: `Заявка ${description}. Номер заявки ${requestId}`,
    });
  };

  const onActionRecall = () => {
    API.requests.recallRequest(requestId, currentUser[2]).then(() => {
      onActionClick("cancel");
    });
  };

  const onActionApprove = async () => {
    await API.requests.approveRequest(requestId, currentUser[2]);

    const response = await API.requests.getRequest(requestId, currentUser[2]);
    setRequestDetails(response);
    setRequestData(JSON.parse(response.requestData) as RequestData);
    openNotification("согласована");
  };

  const onActionDecline = async () => {
    await API.requests.declineRequest(requestId, currentUser[2]);

    const response = await API.requests.getRequest(requestId, currentUser[2]);
    setRequestDetails(response);
    setRequestData(JSON.parse(response.requestData) as RequestData);
    openNotification("отказана");
  };

  return (
    <div style={{ padding: "30px 60px 60px" }}>
      <Typography.Title level={4}>{requestData?.title}</Typography.Title>
      <Divider type="horizontal" orientation="left" plain>
        Заявка
      </Divider>
      <Table
        style={{ padding: "0 30px" }}
        rowKey="name"
        dataSource={requestData?.controls}
        pagination={false}
        showHeader={false}
      >
        <Column title="Поле" dataIndex="label" key="label" />
        <Column
          title="Значение"
          key="value"
          dataIndex="value"
          render={(_: unknown, record: RequestDataControl) => {
            switch (record.type) {
              case "select":
              case "multiselect":
                return (
                  <Typography.Text>
                    {record.options
                      ?.filter(
                        (o) => (record.value as number[]).indexOf(o.id) !== -1,
                      )
                      ?.map((o) => o.text)
                      .join(",")}
                  </Typography.Text>
                );
              default:
                return <Typography.Text>{record.value}</Typography.Text>;
            }
          }}
        />
      </Table>
      <Divider type="horizontal" orientation="left" plain>
        Согласование
      </Divider>
      <Table
        style={{ padding: "0 30px" }}
        rowKey="id"
        dataSource={[
          ...(requestDetails?.primaryApproverGroupLdapUsers ?? []),
          ...(requestDetails?.secondaryApproverGroupLdapUsers ?? []),
        ]}
        pagination={{ position: ["bottomCenter"], pageSize: 20 }}
        showHeader={false}
      >
        <Column title="Согласующий" dataIndex="userName" key="userName" />
        <Column
          title="Статус заявки"
          key="requestApproved"
          dataIndex="isApproved"
          render={(_: unknown, record: ApproverDataType) => (
            <CheckApprove isApproved={record.isApproved} />
          )}
        />
      </Table>
      <Divider type="horizontal" orientation="left" plain>
        Действия
      </Divider>
      {actions === "owner" && (
        <Space>
          <Button htmlType="button" type="primary" onClick={onActionRecall}>
            Отозвать
          </Button>
          <Button htmlType="button" onClick={() => onActionClick("cancel")}>
            Закрыть
          </Button>
        </Space>
      )}
      {actions === "approver" && (
        <Space>
          <Button htmlType="button" type="primary" onClick={onActionApprove}>
            Согласовать
          </Button>
          <Button htmlType="button" type="primary" onClick={onActionDecline}>
            Отказать
          </Button>
          <Button htmlType="button" onClick={() => onActionClick("cancel")}>
            Закрыть
          </Button>
        </Space>
      )}
      {actions === "viewer" && (
        <Space>
          <Button htmlType="button" onClick={() => onActionClick("cancel")}>
            Закрыть
          </Button>
        </Space>
      )}
    </div>
  );
};

export default ViewRequest;
