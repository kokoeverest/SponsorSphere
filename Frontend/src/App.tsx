import React, { useContext } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import AthleteList from "./features/athletes/AthleteList";
import AthleteDetail from "./features/athletes/AthleteDetail";
import Header from "./components/Header";
import LoginForm from "./features/login/LoginForm";
import WelcomePage from "./pages/WelcomePage";
import RegisterChoice from "./components/RegisterChoice";
import RegisterAthlete from "./features/athletes/registration/RegisterAthleteForm";
import RegisterCompany from "./features/sponsors/companies/registration/RegisterSponsorCompanyForm";
import RegisterIndividualForm from "./features/sponsors/individuals/registration/RegisterSponsorIndividualForm";
import "./styles/App.css";
import Dashboard from "./components/Dashboard";
import PrivateRoute from "./utils/PrivateRoute";
import CreateSportEventForm from "./features/sportEvents/CreateSportEventForm";
import CreateAchievementForm from "./features/athletes/achievements/CreateAchievementForm";
import CreateGoalForm from "./features/athletes/goals/CreateGoalForm";
import { Box } from "@mui/material";
import Feed from "./components/Feed";
import AuthContext from "./context/AuthContext";
import PendingSportEventsList from "./features/admins/PendingSportEventsList";

const queryClient = new QueryClient();

const App: React.FC = () =>
{
  const { isLogged } = useContext(AuthContext);

  return (
    <QueryClientProvider client={ queryClient }>
      <Router>
        <Header />
        { isLogged && <Dashboard /> }
        <Box className="main-content">
          <Routes>
            <Route path="/" element={ <WelcomePage /> } />

            <Route path="register">
              <Route index element={ <RegisterChoice /> } />
              <Route path="athlete" element={ <RegisterAthlete /> } />
              <Route path="company" element={ <RegisterCompany /> } />
              <Route path="individual" element={ <RegisterIndividualForm /> } />
            </Route>

            <Route element={ <PrivateRoute /> }>
              <Route path="/feed" element={ <Feed /> } />
              <Route path="/athletes" element={ <AthleteList /> } />
              <Route path="/athletes/:id" element={ <AthleteDetail /> } />
              <Route path="/dashboard" element={ <Dashboard /> } />
              <Route path="/achievements/create" element={ <CreateAchievementForm /> } />
              <Route path="/achievements/sportEvents/create" element={ <CreateSportEventForm /> } />
              <Route path='/goals/create' element={ <CreateGoalForm /> } />
              <Route path='/sportEvents/pending' element={<PendingSportEventsList />}/>

            </Route>

            <Route>

            </Route>
            <Route path="/login" element={ <LoginForm /> } />
          </Routes>
        </Box>
      </Router>
      <ReactQueryDevtools initialIsOpen={ false } />
    </QueryClientProvider>
  );
};

export default App;
