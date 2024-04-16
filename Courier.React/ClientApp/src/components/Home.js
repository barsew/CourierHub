import { Box, Avatar, Button } from '@mui/material';
import { styled } from '@mui/system';
import { useNavigate } from 'react-router-dom';

const RightBottomBox = styled(Box)({
    position: 'absolute',
    width: 'fit-content',
    bottom: 0,
    right: '25%',
    transform: 'translateX(50%)',
});

const StyledAvatar = styled(Avatar)({
    width: '80vh',
    height: '80vh',
    borderRadius: '0px',
});
const StyledButton = styled(Button)({
    position: 'absolute',
    top: '30%',
    left: 0,
    transform: 'translateY(-50%)',
    width: '50%',
    height: '40%',
    borderRadius: '0 50px 50px 0',
    backgroundColor: '#2F1B12',
    padding: '20px',
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    color: 'beige',
    fontSize: '8vh', // Zwi?kszono rozmiar czcionki
    textTransform: 'none',
    border: 'none', // Usuni?to obramowanie
    '&:hover': {
        backgroundColor: '#57403C',
    },
});

const Home = () => {
    const navigate = useNavigate();
    const handleClick = () => {
        navigate('/create-inquiry');
    };

    return (
        <Box>
            <StyledButton onClick={handleClick}>
                Send Package
            </StyledButton>
            <RightBottomBox>
                <StyledAvatar src={require('../pictures/postman_pat_bottom.png')} />
            </RightBottomBox>
        </Box>
    );
};

export default Home;
