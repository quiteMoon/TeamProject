import { useGoogleLogin } from "@react-oauth/google";
import axios from "axios";
import * as React from "react";
import EnvConfig from "../../config/env";

const Login: React.FC = () => {
  const loginByGoogle = useGoogleLogin({
      onSuccess: async tokenResponse => {
        const googleToken = tokenResponse.access_token;
        console.log("Google Token:", googleToken);

        try {
          const response = await axios.post(`${EnvConfig.API_URL}/api/Account/google-login`, {
            token: googleToken,
          });

          console.log("Response from server:", response.data.payload);
        } 
        catch (error) {
          if (axios.isAxiosError(error)) {
            console.error("Login error:", error.response?.data || error.message);
          } else {
            console.error("Unexpected error:", error);
          }
        }
      },
      onError: error => {
        console.error("Google login error:", error);
      },
  });

  return (
    <>
        
        <button onClick={() => loginByGoogle()}>Sign in with Google 🚀</button>
    </>
  );
};

export default Login;
