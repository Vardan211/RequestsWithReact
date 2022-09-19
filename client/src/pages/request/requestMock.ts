import {
  RequestData,
  RequestDataControl,
  RequestDataControlOption,
} from "../../types";

const requestMock = {
  title: "Заявка на отгул",
  controls: [
    {
      name: "author",
      type: "text",
      label: "Автор",
      value: "%currentUser%",
      required: true,
      readonly: true,
    } as RequestDataControl,
    {
      name: "beginDate",
      type: "date",
      label: "Дата начала отгула",
      value: "2022-06-21",
      required: true,
      readonly: false,
    } as RequestDataControl,
    {
      name: "lenght",
      type: "digit",
      label: "Длительность отгула (дней)",
      value: 1,
      required: true,
      readonly: false,
    } as RequestDataControl,
    {
      name: "type",
      type: "select",
      label: "Вид отгула",
      options: [
        {
          id: 0,
          text: "За свой счет",
        } as RequestDataControlOption,
        {
          id: 1,
          text: "За ранее отработанное время",
        } as RequestDataControlOption,
      ],
      value: [1],
      required: true,
      readonly: false,
    } as RequestDataControl,
    {
      name: "reason",
      type: "textarea",
      label: "Причина отгула",
      value: "За работу в майские празники",
      required: true,
      readonly: false,
    } as RequestDataControl,
  ],
  approverGroups: ["pm", "headofdepartment"],
  solutionGroups: ["ceo"],
} as RequestData;

export default requestMock;
