import React from "react";
import cn from "classnames";
import cls from "./detailsview.module.scss";

type DetailsViewProps = {
  title: string;
  isDetailsVisible: boolean;
  setDetailsVisibleFalse: (visibility: boolean) => void;
  renderDetails: () => React.ReactNode;
} & React.HTMLAttributes<HTMLDivElement>;

const DetailsView: React.FC<DetailsViewProps> = ({
  title,
  isDetailsVisible,
  setDetailsVisibleFalse,
  renderDetails,
  children,
}) => (
  <div className={cn(cls.detailsview, { [cls.open]: isDetailsVisible })}>
    <div className={cls.content}>
      <h3 className={cls.title}>{title}</h3>
      {children}
    </div>
    {isDetailsVisible && (
      <div className={cls.popup}>
        {renderDetails()}
        <button type="button" onClick={() => setDetailsVisibleFalse(false)}>
          Закрыть
        </button>
      </div>
    )}
  </div>
);

export default DetailsView;
