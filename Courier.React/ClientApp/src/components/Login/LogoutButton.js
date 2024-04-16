import { useAuth0 } from "@auth0/auth0-react";
import React, { useEffect } from 'react'
import LogoutIcon from '@mui/icons-material/Logout';
import { Button } from '@mui/material'

const LogoutButton = () => {
    const { logout, isAuthenticated } = useAuth0();


    useEffect(() => {
        if (!isAuthenticated) {
            fetch('/api2/login')
                .then((result) => result.json())
                .catch((error) => console.error('Error: ', error));
        }
    }, [isAuthenticated])
    return (
        isAuthenticated && (
            <Button variant="contained"
                startIcon={<LogoutIcon />}
                sx={{ backgroundColor: 'maroon', '&:hover': { backgroundColor: 'red' } }}
                onClick={() => logout()}>
                Sign out
            </Button>
        )
    )
}
export default LogoutButton