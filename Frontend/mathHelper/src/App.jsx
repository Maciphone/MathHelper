import "./App.css";
import { useNavigate, useRoutes } from "react-router-dom";
import Layout from "./Components/Layout";
import Welcome from "./Pages/Welcome";
import Algbera from "./Pages/Algbera";
import Multiplication from "./Pages/Multiplication";
import Division from "./Pages/Division";

import { Login } from "./Pages/Login";
import { CookiesProvider } from "react-cookie";
import RemainDivision from "./Pages/RemainDivision";
import MySolutions from "./Pages/MySolutions";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState, useCallback } from "react";
import { removeName } from "./Reduce/userInformation";
import ExpirationAlert from "./Components/ExpirationAlert";
import First from "./PagesToDelete/First";
import Ai from "./Pages/Ai";
import Registration from "./Pages/Registration.jsx";

function App() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const tokenExpiration = useSelector(
    (state) => state.authData.tokenExpiration
  );
  console.log(tokenExpiration);
  //backend sends in min tokens expire, this useEffect sets a timeout

  //alert modal
  const [showModal, setShowModal] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  //
  useEffect(() => {
    if (!tokenExpiration || !isLoggedIn) return;

    const timeUntillExpiration = tokenExpiration * 60 * 1000; //set to milisecs

    const timeoutSet = setTimeout(() => {
      dispatch(removeName());
      setShowModal(true);
      setIsLoggedIn(false);
    }, timeUntillExpiration);
    return () => clearTimeout(timeoutSet);
  }, [dispatch, tokenExpiration, isLoggedIn]);

  // const handleModalClose = () => {
  //   setShowModal(false); // close alertModal
  // };
  // const handleNavigateLogin = () => {
  //   setShowModal(false);
  //   navigate("/login"); // navigation in modal
  // };
  const handleModalClose = useCallback(() => {
    setShowModal(false);
  }, []);
  const handleNavigateLogin = useCallback(() => {
    setShowModal(false);
    navigate("/login");
  }, [navigate]);

  const routes = useRoutes([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <Welcome />,
        },
        {
          path: "/algebra",
          element: <Algbera />,
        },
        {
          path: "/multiplication",
          element: <Multiplication />,
        },
        {
          path: "/division",
          element: <Division />,
        },

        {
          path: "/maradek",
          element: <RemainDivision />,
        },
        {
          path: "/text",
          element: <Ai />,
        },
        {
          path: "/saját",
          element: <MySolutions />,
        },
        {
          path: "/login",
          element: <Login setIsLoggedIn={setIsLoggedIn} />,
        },
        {
          path: "/register",
          element: <Registration />,
        },
        {
          path: "/todelete",
          element: <First />,
        },
      ],
    },
  ]);
  return (
    <CookiesProvider>
      {routes}
      {showModal && (
        <ExpirationAlert
          onClose={handleModalClose}
          onNavigate={handleNavigateLogin}
        />
      )}
    </CookiesProvider>
  );
}

export default App;
