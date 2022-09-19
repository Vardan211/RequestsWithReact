const mapDepartment = (department: string): string => {
  switch (department) {
    case "dpt_bookkeeping":
      return "Отдел бухгалтерии";
    case "dpt_cpp":
      return "Отдел C++";
    case "dpt_design":
      return "Отдел дизайна";
    case "dpt_maintenance":
      return "Отдел технического обслуживания";
    case "dpt_it":
      return "Отдел IT";
    case "dpt_legal":
      return "Юридический отдел";
    case "dpt_java":
      return "Отдел Java";
    case "dpt_management":
      return "Отдел менеджмента";
    case "dpt_marketing":
      return "Отдел маркетинга";
    case "dpt_mobile":
      return "Отдел мобильной разработки";
    case "dpt_hr":
      return "Отдел HR";
    case "dpt_gamedev":
      return "Отдел геймдева";
    case "dpt_net":
      return "Отдел .Net";
    case "dpt_pm":
      return "Отдел PM";
    case "dpt_qa":
      return "Отдел QA";
    case "dpt_sa":
      return "Отдел аналитики";
    case "dpt_sales":
      return "Отдел продаж";
    case "dpt_web":
      return "Отдел веб разработки";
    case "dpt_datascience":
      return "Отдел Data Science";
    case "dpt_office":
      return "Отдел офис менеджеров";
    case "heads_of_depatments":
      return "Руководитель";
    default:
      return department;
  }
};

export default mapDepartment;
