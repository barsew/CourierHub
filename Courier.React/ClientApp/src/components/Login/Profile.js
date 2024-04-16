import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { styled, Box, Avatar, Typography } from '@mui/material';

const StyledBox = styled(Box)({
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  height: 'calc(100vh - 70px)',
  position: 'relative',
  background: 'linear-gradient(#755139, #feedca)',
});

const ProfileInfo = styled(Box)({
  display: 'flex',
  alignItems: 'center',
  backgroundColor: '#2F1B12',
  color: '#FAF3EB',
  padding: '20px',
  borderRadius: '10px',
  width: '70%',
  height: '40%',
  marginTop: '-30vh',
});

const AvatarContainer = styled(Box)({
  marginRight: '3%',
  marginLeft: '2%',
  marginTop: '3%',
  marginBottom: '3%',
  flex: '0 0 25%',
});

const AvatarImage = styled(Avatar)({
  width: '30vh',
  height: '30vh',
  borderRadius: '3px',
});

const NameTypography = styled(Typography)({
    fontSize: '6vh',
    color: '#FAF3EB', 
    fontFamily: 'Courier New, monospace', 
});

const StyledTypography = styled(Typography)({
    fontSize: '4vh',
    color: '#FAF3EB', 
    fontFamily: 'Courier New, monospace',
});

const Profile = () => {
    const { user, isAuthenticated } = useAuth0();

    return (
        isAuthenticated && (
            <StyledBox>
                <ProfileInfo>
                    <AvatarContainer>
                        <AvatarImage alt={user.name} src={user.picture} />
                    </AvatarContainer>
                    <Box>
                        <NameTypography variant="h6">Name: {user.given_name} {user.family_name}</NameTypography>
                        <StyledTypography variant="body1">E-mail: {user.email}</StyledTypography>
                        <StyledTypography variant="body2">Role: {user.role[0]}</StyledTypography>
                    </Box>
                </ProfileInfo>
            </StyledBox>
        )
    );
};

// reszta kodu pozostaje bez zmian


export default Profile;
