import { useAuth0 } from "@auth0/auth0-react";
import React, { useEffect } from 'react'
import { Button } from '@mui/material'
import LoginIcon from '@mui/icons-material/Login';
import { useNavigate } from 'react-router-dom';

const LoginButton = () => {
    const { user, isAuthenticated, loginWithPopup } = useAuth0();
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthenticated) {
            const getUserFromToken = () => {
                const data = { name: user.name, family_name: user.family_name, sub: user.sub, given_name: user.given_name, email: user.email, phone_number: user.phone_number, role: user.role[0] }
                fetch('/api2/login', {
                    method: 'POST',
                    body: JSON.stringify(data),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                    .then(response => {
                        if (response.ok) {
                            return response.json();
                        } else if (response.status === 403) {
                            return response.json().then(data => {
                                navigate("/userdata-form", { state: { auth0Id: user.sub, incompleteData: data } });
                            });
                        } else {
                            throw new Error('Something went wrong ...');
                        }
                    })
            }
            getUserFromToken();
        }
    }, [isAuthenticated])
    return (
        !isAuthenticated && (
            <Button variant="contained" onClick={() => loginWithPopup()} startIcon={<LoginIcon />}>
                Log in
            </Button>
        )
    )
}

export default LoginButton