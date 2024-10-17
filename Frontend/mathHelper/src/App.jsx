import "./App.css";
import { useRoutes } from "react-router-dom";
import Layout from "./Components/Layout";
import Welcome from "./Pages/Welcome";
import Algbera from "./Pages/Algbera";
import Multiplication from "./Pages/Multiplication";
import Division from "./Pages/Division";
import AITextExercise from "./Pages/TesterPage";
import LevelButtons from "./Components/LevelButtons";
import { Login } from "./Pages/Login";
import { CookiesProvider } from "react-cookie";
import RemainDivision from "./Pages/RemainDivision";

function App() {
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
          path: "/button",
          element: <LevelButtons />,
        },
        {
          path: "/login",
          element: <Login />,
        },
      ],
    },
  ]);
  return <CookiesProvider>{routes}</CookiesProvider>;
}

export default App;
