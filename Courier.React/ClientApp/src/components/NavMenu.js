import { AppBar, Toolbar, Typography, styled, Button, Avatar } from '@mui/material';
import LoginButton from './Login/LoginButton';
import LogoutButton from './Login/LogoutButton';
import { useAuth0 } from "@auth0/auth0-react";
import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';

const StyledToolbar = styled(Toolbar)({
    display: "flex",
    justifyContent: "space-between",
    background: '#2F1B12',
    height: '70px',
    alignItems: 'center'
});

const StyledButton = styled(Button)({
    color: "#FAF3EB",
    '&.MuiButton-root': {
        textTransform: 'none',
        marginRight: '15px',
        fontSize: '18px'
    },
    '&.MuiButton-text': {
        '&:hover': {
            color: '#feedca',
        }
    }
});

const StyledAvatar = styled(Avatar)({
    width: '60px',
    height: '60px',
    marginRight: '10px',
});

const StyledLink = styled(Link)({
    color: '#FAF3EB',
    textDecoration: 'none',
    '&:hover': {
        textDecoration: 'none',
        color: '#FAF3EB',
    },
});

const NavMenu = () => {
    const { isAuthenticated } = useAuth0();
    const navigate = useNavigate();
    const [value, setValue] = useState(0);

    const handleButtonClick = (newValue) => {
        setValue(newValue);
        if (newValue === 0) {
            navigate('/');
        } else if (newValue === 1) {
            navigate('/inquiries-data');
        } else if (newValue === 2) {
            navigate('/orders-data');
        } else if (newValue === 3) {
            navigate('/profile');
        }
    };

    return (
        <AppBar position="static">
            <StyledToolbar>
                <Link to="/">
                    <StyledAvatar alt="Courier Logo" src={require('../pictures/package.png')} />
                </Link>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1, color: "#FAF3EB" }}>
                    <StyledLink to="/">
                        CourierHub
                    </StyledLink>
                </Typography>
              

                {isAuthenticated && (
                    <>
                        <StyledButton onClick={() => handleButtonClick(0)}>Home</StyledButton>
                        <StyledButton onClick={() => handleButtonClick(1)}>Inquiries</StyledButton>
                        <StyledButton onClick={() => handleButtonClick(2)}>Orders</StyledButton>
                        <StyledButton onClick={() => handleButtonClick(3)}>Profile</StyledButton>
                    </>
                )}
                <LoginButton />
                <LogoutButton />
            </StyledToolbar>
        </AppBar>
    );
};

export default NavMenu;
