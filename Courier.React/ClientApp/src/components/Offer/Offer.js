import React, { useState } from 'react';

function Offer({ offer, handleAcceptedOffer, isSelected, handleSelectedIndex, index }) {

    const [acceptedOffer, setAcceptedOffer] = useState(false);
    const { formattedExpireDate, roundedPrice, currency } = {
        formattedExpireDate: new Date(offer.expireDate).toLocaleDateString(),
        roundedPrice: offer.fullPrice.amount.toFixed(2),
        currency: offer.fullPrice.currency
    };
    

    const acceptedOfferStyle = {
        backgroundColor: '#e1cead',
    }
    const notAcceptedOfferStyle = {
       
    }

    const handleAcceptButtonClick = () => {
        setAcceptedOffer(true);
        handleAcceptedOffer(offer);
        handleSelectedIndex(index)
    }

    return (
        <div className="offer" style={isSelected == true ? acceptedOfferStyle : notAcceptedOfferStyle}>
            <h2>Offer</h2>
            <div className="offer-details">
                <div>
                    <strong>Price:</strong> {roundedPrice} {currency}
                </div>
                <div>
                    <strong>Expire Date:</strong> {formattedExpireDate}
                </div>
                <button className="accept-offer-button" onClick={ handleAcceptButtonClick }>Accept</button>
            </div>
        </div>
    );
}

export default Offer;