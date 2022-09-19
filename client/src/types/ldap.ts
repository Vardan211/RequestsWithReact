export type LdapUserType = {
  id: string;
  userName: string;
  groups: [];
};

export type LdapUserGroupsType = {
  groupName: string;
  users: LdapUserType[];
};

export type LdapUserGroupsResponseType = {
  groups: LdapUserGroupsType[];
};
