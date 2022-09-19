import React from "react";
import { Layout, Typography } from "antd";
import HeaderButton from "./components/button";
import cls from "./header.module.scss";
import {
  CREATE_REQUEST,
  MY_REQUESTS,
  REQUESTS_FOR_APPROVE,
  REQUESTS_VIEWER,
} from "../../api/constants/constants";

type HeaderProps = {
  title: string;
  selectedView: number;
  onSelectedViewChanged: (viewIndex: number) => void;
  allowedPages: string[];
};

const Header: React.FC<HeaderProps> = ({
  title,
  selectedView,
  onSelectedViewChanged,
  allowedPages,
}) => (
  <div className={cls.header}>
    <Typography.Title className={cls.title} level={3}>
      {title}
    </Typography.Title>
    <Layout className={cls.buttons}>
      {allowedPages?.indexOf(CREATE_REQUEST) !== -1 && (
        <HeaderButton
          selected={selectedView === 0}
          onClick={() => onSelectedViewChanged(0)}
        >
          Новая заявка
        </HeaderButton>
      )}
      {allowedPages?.indexOf(MY_REQUESTS) !== -1 && (
        <HeaderButton
          selected={selectedView === 1}
          onClick={() => onSelectedViewChanged(1)}
        >
          Мои заявки
        </HeaderButton>
      )}
      {allowedPages?.indexOf(REQUESTS_FOR_APPROVE) !== -1 && (
        <HeaderButton
          selected={selectedView === 2}
          onClick={() => onSelectedViewChanged(2)}
        >
          Я согласую
        </HeaderButton>
      )}
      {allowedPages?.indexOf(REQUESTS_VIEWER) !== -1 && (
        <HeaderButton
          selected={selectedView === 3}
          onClick={() => onSelectedViewChanged(3)}
        >
          Все заявки
        </HeaderButton>
      )}
    </Layout>
  </div>
);

export default Header;
