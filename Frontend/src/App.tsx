import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import AthleteList from './features/athletes/AthleteList';
import AthleteDetail from './features/athletes/AthleteDetail';
import Header from './components/Header';
import LoginForm from './features/login/LoginForm';
import WelcomePage from './pages/WelcomePage';
import RegisterChoice from './components/RegisterChoice';
import RegisterAthlete from './features/athletes/registration/RegisterAthleteForm';
import RegisterCompany from './features/sponsors/companies/registration/RegisterSponsorCompanyForm';
import RegisterIndividualForm from './features/sponsors/individuals/registration/RegisterSponsorIndividualForm';
import './styles/App.css';
import Dashboard from './components/Dashboard';

const queryClient = new QueryClient()

const App: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(!!localStorage.getItem('token'));

  return (
    <QueryClientProvider client={queryClient}>
    <Router>
      <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
      <div className="main-content">
        <Routes>
          <Route path="/" element={<WelcomePage />} />
          <Route path="register">
            <Route index element={<RegisterChoice />} />
            <Route path="athlete" element={<RegisterAthlete />} />
            <Route path="company" element={<RegisterCompany />} />
            <Route path="individual" element={<RegisterIndividualForm />} />
          </Route>
          <Route path="/athletes" element={<AthleteList />} />
          <Route path="/athletes/:id" element={<AthleteDetail />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path='/dashboard' element={<Dashboard/>} />
        </Routes>
      </div>
    </Router>
      <ReactQueryDevtools initialIsOpen={ false } />
    </QueryClientProvider>
  );
};

export default App;