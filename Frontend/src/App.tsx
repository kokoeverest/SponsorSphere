import React from "react";
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
import PrivateRoute from "./utils/PrivateRoute";
import CreateSportEventForm from "./features/sportEvents/CreateSportEventForm";
import CreateAchievementForm from "./features/athletes/achievements/CreateAchievementForm";
import CreateGoalForm from "./features/athletes/goals/CreateGoalForm";
import { Box } from "@mui/material";
import Feed from "./components/Feed";
import PendingSportEventsList from "./features/admins/PendingSportEventsList";
import UpdateSportEventForm from "./features/sportEvents/UpdateSportEventForm";
import CreateBlogPostForm from "./features/blogPosts/CreateBlogPostForm";
import CreateSponsorshipForm from "./features/sponsorships/CreateSponsorshipForm";
import UpdateAthleteProfileForm from "./features/athletes/UpdateAthleteProfileForm";
import SponsorCompanyDetail from "./features/sponsors/companies/SponsorCompanyDetail";
import SponsorIndividualDetail from "./features/sponsors/individuals/SponsorIndividualDetail";
import { PendingSportEventsProvider } from "./context/PendingSportEventsContext";

const queryClient = new QueryClient();

const App: React.FC = () =>
{
  return (
    <QueryClientProvider client={ queryClient }>
      <Router>
        <Header />
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
              <Route path="/achievements/create" element={ <CreateAchievementForm /> } />
              <Route path="/achievements/sportEvents/create" element={ <CreateSportEventForm /> } />
              <Route path='/goals/create' element={ <CreateGoalForm /> } />

              <Route path='/blogposts/create' element={ <CreateBlogPostForm /> } />
              <Route path='sponsorships/create' element={ <CreateSponsorshipForm /> } />
              <Route path='/users/profile/update' element={ <UpdateAthleteProfileForm /> } />
              <Route path='/sponsors/companies/:sponsorId' element={ <SponsorCompanyDetail /> } />
              <Route path='/sponsors/individuals/:sponsorId' element={ <SponsorIndividualDetail /> } />
              <Route path='/sportEvents/pending' element={ <PendingSportEventsList /> } />

              <Route element={ <PendingSportEventsProvider children={ <UpdateSportEventForm /> } /> }>
                <Route path='/sportEvents/update' element={ <UpdateSportEventForm /> } />
              </Route>

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
