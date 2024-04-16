import React, { useState } from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';
import Offer from './Offer';
import { useAuth0 } from "@auth0/auth0-react";
import { Box, styled } from '@mui/material';

const StyledBox = styled(Box)({
    background: 'linear-gradient(#755139, #feedca)',
});
function Offers() {

    const navigate = useNavigate();
    const location = useLocation()
    const offers = location.state?.offers;
    const inquiryData = location.state?.inquiry;
    const [acceptedOffer, setAcceptedOffer] = useState(null);
    const { isAuthenticated } = useAuth0();
    const [selectedIndex, setSelectedIndex] = useState(null);

    const onClickNextButton = () => {
        if (!isAuthenticated) {
            navigate("/login-page", { state: { acceptedOffer: acceptedOffer, inquiry: inquiryData } });
        }
        else {
            navigate("/sender-form", { state: { acceptedOffer: acceptedOffer, inquiry: inquiryData } });  
        }
    }

    return (
        <div>
            <div className="offersCards">
                {offers.map((offer, index) => (
                    <div key={ index }>
                        <Offer offer={offer} handleAcceptedOffer={setAcceptedOffer} isSelected={selectedIndex === index} handleSelectedIndex={setSelectedIndex} index={index} />
                    </div>
                ))}
            </div>
            <div className="container-forms-buttons">
                <button className="btn btn-danger" id="back-button" onClick={() => navigate(-1)}>Go back</button>
                <button className="btn btn-primary" disabled={acceptedOffer === null} id="next-button" onClick={onClickNextButton}>Next</button>
            </div>
        </div>
    );
}

export default Offers;