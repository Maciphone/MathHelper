import "./App.css";
import { useNavigate, useRoutes } from "react-router-dom";
import Layout from "./Components/Layout";
import Welcome from "./Pages/Welcome";
import Algbera from "./Pages/Algbera";
import Multiplication from "./Pages/Multiplication";
import Division from "./Pages/Division";
import AITextExercise from "./PagesToDelete/TesterPage";

import { Login } from "./Pages/Login";
import { CookiesProvider } from "react-cookie";
import RemainDivision from "./Pages/RemainDivision";
import MySolutions from "./Pages/MySolutions";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { removeName } from "./Reduce/userInformation";
import ExpirationAlert from "./Components/ExpirationAlert";
import First from "./PagesToDelete/First";

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

  //
  useEffect(() => {
    if (!tokenExpiration) return;

    const timeUntillExpiration = tokenExpiration * 60 * 1000; //set to milisecs

    const timeoutSet = setTimeout(() => {
      dispatch(removeName());
      setShowModal(true);
    }, timeUntillExpiration);
    return () => clearTimeout(timeoutSet);
  }, [tokenExpiration, dispatch, navigate]);

  const handleModalClose = () => {
    setShowModal(false); // close alertModal
  };

  const handleNavigateLogin = () => {
    setShowModal(false);
    navigate("/login"); // navigation in modal
  };

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
          path: "/text",
          element: <AITextExercise />,
        },
        {
          path: "/tester",
          element: <AITextExercise />,
        },
        {
          path: "/maradek",
          element: <RemainDivision />,
        },
        {
          path: "/saját",
          element: <MySolutions />,
        },
        {
          path: "/login",
          element: <Login />,
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
