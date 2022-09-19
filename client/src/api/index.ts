import axios from "axios";
import RequestsApi from "./request-template";

const axiosInstance = axios.create({
  baseURL: "https://portal.osinit.com:9091",
  headers: {
    "Content-type": "application/json",
  },
});

const API = {
  requests: new RequestsApi(axiosInstance),
};

export default API;

export * from "./constants/constants";
