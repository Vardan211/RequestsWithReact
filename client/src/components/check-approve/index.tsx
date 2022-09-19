/* eslint-disable no-nested-ternary */
import { Typography } from "antd";
import React from "react";

type CheckApproveProps = {
  // eslint-disable-next-line react/require-default-props
  isApproved?: boolean;
};

const CheckApprove: React.FC<CheckApproveProps> = ({ isApproved = null }) => (
  <Typography.Text>
    {isApproved === null
      ? "Ожидает согласования"
      : isApproved
      ? "Согласована"
      : "Не согласована"}
  </Typography.Text>
);

export default CheckApprove;
