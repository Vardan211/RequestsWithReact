import { Axios } from "axios";
import {
  LdapUserGroupsResponseType,
  LdapUserType,
  CreateRequestResponseType,
  CreateRequestType,
  RequestDetailsType,
  Requests,
  RequestTemplateExtra,
  RequestTemplateList,
} from "../types";

export default class RequestsApi {
  private readonly axios;

  constructor(axios: Axios) {
    this.axios = axios;
  }

  getRequestTemplates = async (): Promise<RequestTemplateList> => {
    const response = await this.axios.get("/v1/RequestTemplates");
    return response.data;
  };

  getRequestTemplate = async (id: number): Promise<RequestTemplateExtra> => {
    const response = await this.axios.get(`/v1/RequestTemplates/${id}`);
    return response.data;
  };

  getRequests = async (ldapUserId: string): Promise<Requests> => {
    const response = await this.axios.get(`/v1/Requests`, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
    return response.data;
  };

  getRequest = async (
    id: number,
    ldapUserId: string,
  ): Promise<RequestDetailsType> => {
    const response = await this.axios.get(`/v1/Requests/${id}`, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
    return response.data;
  };

  createRequest = async (
    createRequest: CreateRequestType,
    ldapUserId: string,
  ): Promise<CreateRequestResponseType> => {
    const response = await this.axios.post("/v1/Requests", createRequest, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
    return response.data;
  };

  getLdapUser = async (email: string): Promise<LdapUserType> => {
    const response = await this.axios.get(
      `/v1/Ldap/getUser?userIdentity=${email}`,
    );
    return response.data;
  };

  getLdapUsers = async (
    groups: string[],
  ): Promise<LdapUserGroupsResponseType> => {
    const response = await this.axios.get(
      `/v1/Ldap/getUsers?${groups.map((g) => `groupNames=${g}`).join("&")}`,
    );
    return response.data;
  };

  getTabs = async (ldapUserId: string): Promise<string[]> => {
    const response = await this.axios.get(`v1/Settings/tabs`, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
    return response.data;
  };

  approveRequest = async (id: number, ldapUserId: string): Promise<void> => {
    await this.axios.post(`v1/Requests/${id}/approve`, null, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
  };

  declineRequest = async (id: number, ldapUserId: string): Promise<void> => {
    await this.axios.post(`v1/Requests/${id}/decline`, null, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
  };

  recallRequest = async (id: number, ldapUserId: string): Promise<void> => {
    await this.axios.post(`v1/Requests/${id}/recall`, null, {
      headers: {
        LdapUserId: ldapUserId,
      },
    });
  };
}
