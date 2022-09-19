import React from "react";
import moment from "moment";
import {
  Button,
  DatePicker,
  Divider,
  Form,
  Input,
  InputNumber,
  notification,
  Select,
  Space,
  Typography,
} from "antd";
import API from "../../api";
import AppContext from "../../context";
import {
  LdapUserGroupsResponseType,
  RequestData,
  RequestDataControl,
  CreateRequestType,
  Approver,
} from "../../types";
import mapDepartment from "../../utilities/nameMaping";

type CreateRequestProps = {
  actions: "approver" | "owner";
  request: RequestData;
  templateId: number;
  onActionClick: (action: "cancel" | "approve" | "decline" | "recall") => void;
} & React.HTMLAttributes<HTMLDivElement>;

const CreateRequest: React.FC<CreateRequestProps> = ({
  actions,
  request,
  templateId,
  onActionClick,
}) => {
  const currentUser = React.useContext(AppContext);
  const [form] = Form.useForm();
  const [groups, setGroups] = React.useState<LdapUserGroupsResponseType>();
  const [approverGroupsSelection, setApproverGroupsSelection] = React.useState<
    Approver[]
  >([]);
  const [solutionGroupsSelection, setSolutionGroupsSelection] = React.useState<
    Approver[]
  >([]);

  React.useEffect(() => {
    const checkTransform = (
      value: string | number | number[] | undefined,
    ): string | number | number[] | undefined => {
      if (typeof value === "string" && value === "%currentUser%") {
        return currentUser[1];
      }
      return value;
    };

    API.requests
      .getLdapUsers([
        ...request.approverGroups,
        ...(request.solutionGroups ?? []),
      ])
      .then((res) => setGroups(res));

    request.controls.map((control) =>
      form.setFieldsValue({
        [control.name]:
          control.type === "date" ? moment() : checkTransform(control.value),
      }),
    );
  }, [
    currentUser,
    form,
    request.approverGroups,
    request.controls,
    request.solutionGroups,
  ]);

  const openNotification = (requestId: number): void => {
    notification.open({
      message: "Успех",
      description: `Заявка отправлена. Ваш номер заявки ${requestId}`,
    });
  };

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const onFinish = (values: any): void => {
    request.controls = request.controls.map(
      (control) =>
        ({
          ...control,
          value: values[control.name]?.toString(),
        } as RequestDataControl),
    );

    API.requests
      .createRequest(
        {
          requestData: JSON.stringify(request),
          requestTemplateId: templateId,
          primaryApprovers: approverGroupsSelection,
          secondaryApprovers: solutionGroupsSelection,
        } as CreateRequestType,
        currentUser[2],
      )
      .then((res) => {
        form.resetFields();
        openNotification(res.requestId);
      });
  };

  const renderControl = (
    control: RequestDataControl,
    index: number,
  ): React.ReactNode => {
    switch (control.type) {
      case "text":
        return (
          <Form.Item
            key={index}
            label={control.label}
            name={control.name}
            rules={[
              { required: control.required, message: "Необходимо заполнить" },
            ]}
          >
            <Input disabled={control.readonly} />
          </Form.Item>
        );
      case "textarea":
        return (
          <Form.Item
            key={index}
            label={control.label}
            name={control.name}
            rules={[
              { required: control.required, message: "Необходимо заполнить" },
            ]}
          >
            <Input.TextArea rows={4} disabled={control.readonly} />
          </Form.Item>
        );
      case "digit":
        return (
          <Form.Item
            key={index}
            label={control.label}
            name={control.name}
            style={{ alignItems: "flex-start" }}
            rules={[
              { required: control.required, message: "Необходимо заполнить" },
            ]}
          >
            <InputNumber disabled={control.readonly} />
          </Form.Item>
        );
      case "date":
        return (
          <Form.Item
            key={index}
            label={control.label}
            name={control.name}
            style={{ alignItems: "flex-start" }}
            rules={[
              { required: control.required, message: "Необходимо заполнить" },
            ]}
          >
            <DatePicker disabled={control.readonly} />
          </Form.Item>
        );
      case "select":
        return (
          <Form.Item
            key={index}
            label={control.label}
            name={control.name}
            style={{ width: "50%" }}
            rules={[
              { required: control.required, message: "Необходимо заполнить" },
            ]}
          >
            <Select disabled={control.readonly}>
              {control.options?.map((option) => (
                <Select.Option key={option.id} value={option.id}>
                  {option.text}
                </Select.Option>
              ))}
            </Select>
          </Form.Item>
        );
      case "multiselect":
        return (
          <Form.Item
            key={index}
            label={control.label}
            name={control.name}
            rules={[
              { required: control.required, message: "Необходимо заполнить" },
            ]}
          >
            <Select mode="multiple" disabled={control.readonly}>
              {control.options?.map((option) => (
                <Select.Option key={option.id} value={option.id}>
                  {option.text}
                </Select.Option>
              ))}
            </Select>
          </Form.Item>
        );
      default:
        throw Error();
    }
  };

  const renderApproverGroups = (
    approverGroup: string,
    index: number,
  ): React.ReactNode => (
    <Form.Item
      key={index}
      label={mapDepartment(approverGroup)}
      name={approverGroup}
      rules={[{ required: true, message: "Необходимо заполнить" }]}
    >
      <Select
        mode="multiple"
        placeholder="Выберите согласующего"
        onSelect={(value: string) =>
          setApproverGroupsSelection([
            ...approverGroupsSelection,
            { ldapUserId: value, groupName: approverGroup } as Approver,
          ])
        }
        onDeselect={(value: string) =>
          setApproverGroupsSelection(
            approverGroupsSelection.filter((x) => x.ldapUserId !== value),
          )
        }
      >
        {groups?.groups
          ?.filter((g) => g.groupName === approverGroup)
          ?.map((g) => g.users)[0]
          ?.map((option) => (
            <Select.Option key={option.id} value={option.id}>
              {option.userName}
            </Select.Option>
          ))}
      </Select>
    </Form.Item>
  );

  const renderSolutionGroups = (
    solutionGroup: string,
    index: number,
  ): React.ReactNode => (
    <Form.Item
      key={index}
      label={mapDepartment(solutionGroup)}
      name={solutionGroup}
      rules={[{ required: true, message: "Необходимо заполнить" }]}
    >
      <Select
        mode="multiple"
        placeholder="Выберите согласующего"
        onSelect={(value: string) =>
          setSolutionGroupsSelection([
            ...solutionGroupsSelection,
            { ldapUserId: value, groupName: solutionGroup } as Approver,
          ])
        }
        onDeselect={(value: string) =>
          setSolutionGroupsSelection(
            solutionGroupsSelection.filter((x) => x.ldapUserId !== value),
          )
        }
      >
        {groups?.groups
          ?.filter((g) => g.groupName === solutionGroup)
          ?.map((g) => g.users)[0]
          ?.map((option) => (
            <Select.Option key={option.id} value={option.id}>
              {option.userName}
            </Select.Option>
          ))}
      </Select>
    </Form.Item>
  );

  return (
    <Form form={form} layout="vertical" onFinish={onFinish} autoComplete="off">
      <Typography.Title level={4}>{request.title}</Typography.Title>
      <Divider type="horizontal" orientation="left" plain>
        Заявка
      </Divider>
      {request.controls.map(renderControl)}
      <Divider type="horizontal" orientation="left" plain>
        Согласование
      </Divider>
      {request.approverGroups.map(renderApproverGroups)}
      {request.solutionGroups?.map(renderSolutionGroups)}
      <Divider type="horizontal" orientation="left" plain>
        Действия
      </Divider>
      {actions === "owner" && (
        <Space>
          <Button htmlType="submit" type="primary">
            Отправить
          </Button>
          <Button htmlType="button" onClick={() => onActionClick("cancel")}>
            Отменить
          </Button>
        </Space>
      )}
    </Form>
  );
};

type RequestProps = {
  templateId: number;
  mode: "create" | "view";
  actions: "approver" | "owner";
  onActionClick: (action: "cancel" | "approve" | "decline" | "recall") => void;
} & React.HTMLAttributes<HTMLDivElement>;

const Request: React.FC<RequestProps> = ({
  templateId,
  mode,
  actions,
  onActionClick,
}) => {
  const currentUser = React.useContext(AppContext);
  const [requestData, setRequestData] = React.useState<RequestData>();

  React.useEffect(() => {
    if (mode === "create") {
      API.requests.getRequestTemplate(templateId).then((res) => {
        setRequestData(JSON.parse(res.template) as RequestData);
      });
    } else {
      API.requests.getRequest(templateId, currentUser[2]).then((res) => {
        setRequestData(JSON.parse(res.requestData) as RequestData);
      });
    }
  }, [currentUser, mode, templateId]);

  if (!requestData) {
    return <Typography.Text>Загрузка</Typography.Text>;
  }

  switch (mode) {
    case "create":
      return (
        <CreateRequest
          actions={actions}
          request={requestData}
          templateId={templateId}
          onActionClick={onActionClick}
        />
      );
    case "view":
      return (
        <CreateRequest
          actions={actions}
          request={requestData}
          templateId={templateId}
          onActionClick={onActionClick}
        />
      );
    default:
      throw Error();
  }
};

export default Request;
