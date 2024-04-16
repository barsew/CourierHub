import LoginButton from './LoginButton';
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth0 } from "@auth0/auth0-react";
import React, { useEffect } from 'react'

const LoginPage = () => {
    const navigate = useNavigate();
    const location = useLocation()
    const acceptedOffer = location.state?.acceptedOffer;
    const inquiryData = location.state?.inquiry;
    const { isAuthenticated } = useAuth0();

    const handleContinueWithoutLogin = () => {
        navigate("/sender-form", { state: { acceptedOffer: acceptedOffer, inquiry: inquiryData } });
    };

    useEffect(() => {
        if (isAuthenticated) {
            navigate("/sender-form", { state: { acceptedOffer: acceptedOffer, inquiry: inquiryData } })
            }
    }, [isAuthenticated])

    return (
        <div className="login-page-container">
            <div className="login-page-content">
                <h1>Nie jesteś zalogowany.</h1>
                <h1>Czy chcesz się zalogować?</h1>
                <LoginButton />
                <p>
                    <button onClick={handleContinueWithoutLogin} className="invisible-button">
                        Kontynuuj bez logowania
                    </button>
                </p>
            </div>
        </div>
    );
};

export default LoginPage;
