import "./App.css";
import { useRoutes } from "react-router-dom";
import Layout from "./Components/Layout";
import Welcome from "./Pages/Welcome";
import Algbera from "./Pages/Algbera";
import Multiplication from "./Pages/Multiplication";
import Division from "./Pages/Division";
import AITextExercise from "./Pages/AITextExercise";

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
      ],
    },
  ]);
  return routes;
}

export default App;
