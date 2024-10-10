import React from "react";

// Don't forget to
// // download the CSS file too OR
// // remove the following line if you're already using Tailwind
import "../styles.css";

export const Login = () => {
  return (
    <div id="webcrumbs">
      <div className="w-[400px] bg-neutral-50 min-h-[500px] p-6 rounded-lg shadow-lg flex flex-col items-center justify-between">
        <h1 className="text-2xl font-title mb-6">Login</h1>

        <form className="w-full flex flex-col gap-4">
          <div className="flex flex-col gap-1">
            <label htmlFor="email" className="font-semibold">
              Email
            </label>
            <input
              type="email"
              id="email"
              name="email"
              className="p-2 border rounded-md w-full"
              placeholder="Enter your email"
              required
            />
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="password" className="font-semibold">
              Password
            </label>
            <input
              type="password"
              id="password"
              name="password"
              className="p-2 border rounded-md w-full"
              placeholder="Enter your password"
              required
            />
          </div>

          <button
            type="submit"
            className="w-full bg-primary text-white py-3 rounded-md mt-4"
          >
            Log In
          </button>
        </form>

        <p className="mt-6 text-neutral-700">
          Don't have an account?
          <a href="/register" className="text-primary ml-1">
            Register
          </a>
        </p>
      </div>
    </div>
  );
};
