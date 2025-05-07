import axios, { AxiosError } from "axios";
import Config from "../config";
import { useQuery } from "@tanstack/react-query";
import { Claim } from "../types/claim";

const useFetchUser = () => {
  return useQuery<Claim[], AxiosError>({
    queryKey: ["user"],
    queryFn: () =>
      axios
        .get(`${Config.baseApiUrl}/account/getuser?slide=false`)
        .then((resp) => resp.data),
  });
};
export default useFetchUser;