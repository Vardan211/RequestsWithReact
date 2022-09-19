import React, { useState } from "react";
import { Typography } from "antd";
import API from "../../api";
import Request from "../request";
import { RequestTemplate } from "../../types/types";
import cls from "./createrequestview.module.scss";

const CreateRequestView: React.FC = () => {
  const [templates, setTemplates] = React.useState<RequestTemplate[]>([]);
  const [templateId, setTemplateId] = useState<number>(-1);

  React.useEffect(() => {
    API.requests
      .getRequestTemplates()
      .then((data: { templates: React.SetStateAction<RequestTemplate[]> }) =>
        setTemplates(data.templates),
      );
  }, []);

  const onActionClick = (action: string): void => {
    if (action === "cancel") {
      setTemplateId(-1);
    }
  };

  if (templateId > 0) {
    return (
      <div className={cls.createrequest}>
        <Request
          mode="create"
          actions="owner"
          templateId={templateId}
          onActionClick={onActionClick}
        />
      </div>
    );
  }

  return (
    <div className={cls.createrequest}>
      <Typography.Title level={4}>Выберите тип заявки</Typography.Title>
      <div className={cls.templatelist}>
        {templates.map((template) => (
          <Typography.Text
            className={cls.templateitem}
            key={template.id}
            onClick={() => setTemplateId(template.id)}
          >
            #{template.id} {template.name}
          </Typography.Text>
        ))}
      </div>
    </div>
  );
};

export default CreateRequestView;
