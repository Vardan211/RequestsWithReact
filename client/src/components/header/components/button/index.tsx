import React from "react";
import cn from "classnames";
import cls from "./button.module.scss";

type HeaderButtonProps = {
  selected: boolean;
} & React.ButtonHTMLAttributes<HTMLButtonElement>;

const HeaderButton: React.FC<HeaderButtonProps> = ({ selected, ...props }) => (
  <button
    type="button"
    className={cn(cls.button, { [cls.selected]: selected })}
    {...props}
  />
);

export default HeaderButton;
