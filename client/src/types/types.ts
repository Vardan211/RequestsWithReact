export type RequestTemplate = {
  id: number;
  name: string;
};

export type RequestTemplateList = {
  templates: RequestTemplate[];
};

export type RequestTemplateExtra = {
  id: number;
  name: string;
  template: string;
};

export type MyRequest = {
  id: number;
  name: string;
  isApproved: boolean;
};

export type RequestForApprove = {
  id: number;
  name: string;
  authorName: string;
  isApproved: boolean;
};

export type Requests = {
  requests: MyRequest[];
  requestForApprove: RequestForApprove[];
  allRequests: RequestForApprove[];
};

export type Approver = {
  ldapUserId: string;
  groupName: string;
};

export type CreateRequestType = {
  requestTemplateId: number;
  requestData: string;
  primaryApprovers: Approver[];
  secondaryApprovers?: Approver[];
};

export type CreateRequestResponseType = {
  requestId: number;
};

export type RequestDetailsApproverType = {
  id: string;
  userName: string;
  isApproved: boolean;
};

export type RequestDetailsType = {
  id: number;
  requestData: string;
  ldapUserId: string;
  authorName: string;
  isApproved: boolean;
  primaryApproverGroupLdapUsers: RequestDetailsApproverType[];
  secondaryApproverGroupLdapUsers?: RequestDetailsApproverType[];
};
