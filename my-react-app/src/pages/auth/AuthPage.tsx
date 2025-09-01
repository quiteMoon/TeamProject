import React, { useState } from "react";
import { useGoogleLogin } from "@react-oauth/google";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import EnvConfig from "../../config/env";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import type { InferType } from "yup";
import type { Resolver } from "react-hook-form";

const schema = yup.object().shape({
  email: yup.string().email("Невалідна пошта").required("Обов'язкове поле"),
  password: yup.string().min(6, "Мінімум 6 символів").required("Обов'язкове поле"),
  firstName: yup.string().when("isLoginMode", {
    is: false,
    then: (schema) => schema.required("Ім’я обов'язкове"),
  }),
  lastName: yup.string().when("isLoginMode", {
    is: false,
    then: (schema) => schema.required("Прізвище обов'язкове"),
  }),
  image: yup.mixed().nullable(),
  isLoginMode: yup.boolean(),
});

export type AuthFormValues = InferType<typeof schema>;

const AuthPage: React.FC = () => {
  const navigate = useNavigate();
  const [isLoginMode, setIsLoginMode] = useState(true); 

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<AuthFormValues>({
    resolver: yupResolver(schema) as Resolver<AuthFormValues>,
    defaultValues: {
      email: "",
      password: "",
      firstName: "",
      lastName: "",
      image: undefined,
      isLoginMode: true,
    },
  });

  const loginByGoogle = useGoogleLogin({
    onSuccess: async (tokenResponse) => {
      const googleToken = tokenResponse.access_token;
      console.log("Google Token:", googleToken);

      try {
        const response = await axios.post(`${EnvConfig.API_URL}/api/Account/google-login`, {
          token: googleToken,
        });

        console.log("Response from server:", response.data.payload);
      } catch (error) {
        if (axios.isAxiosError(error)) {
          console.error("Login error:", error.response?.data || error.message);
        } else {
          console.error("Unexpected error:", error);
        }
      }
    },
    onError: (error) => {
      console.error("Google login error:", error);
    },
  });

  const OnSubmit = async (data: any) => {
    try {
      if (isLoginMode) {
        const loginData = {
          email: data.email,
          password: data.password,
        };

        console.log("Login data:", loginData);
        const response = await axios.post(`${EnvConfig.API_URL}/api/Account/login`, loginData);

        console.log("Login response:", response.data);
        if (response.data.isSuccess) {
          navigate("/");
        } else {
          alert("Помилка авторизації: " + response.data.message);
        }
      } else {
        const formData = new FormData();
        formData.append("email", data.email);
        formData.append("password", data.password);
        formData.append("firstName", data.firstName);
        formData.append("lastName", data.lastName);
        if (data.avatar?.[0]) {
          formData.append("image", data.image[0]);
        }
  
        console.log("Register data:", Object.fromEntries(formData.entries()));

        const response = await axios.post(`${EnvConfig.API_URL}/api/Account/register`, formData);

        console.log("Register response:", response.data);

        if (response.data.isSuccess) {
          navigate("/");
        } else {
          alert("Помилка реєстрації: " + response.data.message);
        }
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Registration error:", error.response?.data || error.message);
      } else {
        console.error("Unexpected error:", error);
      }
    }
  };  

  return (
    <div className="h-full flex items-center justify-center px-4 overflow-hidden pt-16">
      <div className="w-full max-w-md">
        <h2 className="text-3xl font-bold text-center mb-6">
          {isLoginMode ? "Увійти" : "Зареєструватися"}
        </h2>

        <form onSubmit={handleSubmit(OnSubmit)} className="space-y-5 w-full">
          {/* Email */}
          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700">
              Електронна пошта
            </label>
            <input
              type="email"
              id="email"
              {...register("email")}
              className="mt-1 block w-full px-4 py-2 border rounded-md shadow-sm focus:outline-none focus:ring focus:ring-yellow-400"
            />
            {errors.email && <p className="text-red-500 text-sm mt-1">{errors.email.message}</p>}
          </div>

          {/* Password */}
          <div>
            <label htmlFor="password" className="block text-sm font-medium text-gray-700">
              Пароль
            </label>
            <input
              type="password"
              id="password"
              {...register("password")}
              className="mt-1 block w-full px-4 py-2 border rounded-md shadow-sm focus:outline-none focus:ring focus:ring-yellow-400"
            />
            {errors.password && (
              <p className="text-red-500 text-sm mt-1">{errors.password.message}</p>
            )}
          </div>

          {!isLoginMode && (
            <>
              <div>
                <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">
                  Ім’я
                </label>
                <input
                  type="text"
                  id="firstName"
                  {...register("firstName")}
                  className="mt-1 block w-full px-4 py-2 border rounded-md shadow-sm focus:outline-none focus:ring focus:ring-yellow-400"
                />
                {errors.firstName && (
                  <p className="text-red-500 text-sm mt-1">{errors.firstName.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">
                  Прізвище
                </label>
                <input
                  type="text"
                  id="lastName"
                  {...register("lastName")}
                  className="mt-1 block w-full px-4 py-2 border rounded-md shadow-sm focus:outline-none focus:ring focus:ring-yellow-400"
                />
                {errors.lastName && (
                  <p className="text-red-500 text-sm mt-1">{errors.lastName.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="avatar" className="block text-sm font-medium text-gray-700">
                  Фото профілю
                </label>
                <input
                  type="file"
                  id="image"
                  accept="image/*"
                  {...register("image")}
                  className="mt-1 block w-full text-sm text-gray-700 file:mr-4 file:py-2 file:px-4
                    file:rounded-md file:border-0 file:text-sm file:font-semibold
                    file:bg-yellow-400 file:text-white hover:file:bg-yellow-500"
                />
                {errors.image && (
                  <p className="text-red-500 text-sm mt-1">{errors.image.message}</p>
                )}
              </div>
            </>
          )}

          <input type="hidden" value={isLoginMode.toString()} {...register("isLoginMode")} />

          <button
            type="submit"
            className="w-full py-2 bg-yellow-400 hover:bg-yellow-500 text-white font-semibold rounded-md transition"
          >
            {isLoginMode ? "Увійти" : "Зареєструватися"}
          </button>

          <div className="text-center text-sm text-gray-500">або</div>

          <button
            type="button"
            onClick={() => loginByGoogle()}
            className="w-full py-2 border border-gray-300 hover:bg-gray-100 text-sm font-medium rounded-md flex items-center justify-center gap-2"
          >
            <img
              src="https://upload.wikimedia.org/wikipedia/commons/c/c1/Google_%22G%22_logo.svg"
              alt="Google"
              className="h-5 w-5"
            />
            Продовжити з Google
          </button>

          <div className="text-center mt-4">
            <span className="text-sm text-gray-600">
              {isLoginMode ? "Немає акаунту?" : "Вже маєш акаунт?"}
            </span>
            <button
              type="button"
              onClick={() => {
                setIsLoginMode(!isLoginMode);
                setValue("isLoginMode", !isLoginMode);
              }}
              className="ml-1 text-sm text-yellow-500 hover:underline"
            >
              {isLoginMode ? "Зареєструватися" : "Увійти"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AuthPage;
