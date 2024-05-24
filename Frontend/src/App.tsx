// src/App.tsx
import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './styles/App.css';
import AthleteList from './features/athletes/AthleteList';
import AthleteDetail from './features/athletes/AthleteDetail';
import Header from './components/Header';
import LoginForm from './features/login/LoginForm';
import WelcomePage from './pages/WelcomePage';
import RegisterChoice from './components/RegisterChoice';
import RegisterAthlete from './features/athletes/registration/RegisterAthleteForm';
import RegisterCompany from './features/sponsors/companies/registration/RegisterSponsorCompanyForm';
import RegisterIndividual from './features/sponsors/individuals/registration/RegisterSponsorIndividualForm';


const App: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(!!localStorage.getItem('token'));

  return (
    <Router>
      <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
      <div className="main-content">
        <Routes>
          <Route path="/" element={<WelcomePage />} />
          <Route path="register">
            <Route index element={<RegisterChoice />} />
            <Route path="athlete" element={<RegisterAthlete />} />
            <Route path="company" element={<RegisterCompany />} />
            <Route path="individual" element={<RegisterIndividual />} />
          </Route>
          <Route path="/athletes" element={<AthleteList />} />
          <Route path="/athletes/:id" element={<AthleteDetail />} />
          <Route path="/login" element={<LoginForm />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;