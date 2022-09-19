export type RequestDataControlOption = {
  id: number;
  text: string;
};

export type RequestDataControl = {
  name: string;
  type: "text" | "date" | "digit" | "select" | "multiselect" | "textarea";
  label: string;
  value?: string | number | number[];
  required: boolean;
  readonly: boolean;
  options?: RequestDataControlOption[];
};

export type RequestData = {
  title: string;
  controls: RequestDataControl[];
  approverGroups: string[];
  solutionGroups?: string[];
};
