import React from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';

function InquirySummary() {

    const navigate = useNavigate();
    const location = useLocation()
    const acceptedOffer = location.state?.acceptedOffer;
    const inquiryData = location.state?.inquiry;
    const senderData = location.state?.senderData;
    const recipientData = location.state?.recipientData;

    //console.log(acceptedOffer);
    //console.log(inquiryData);
    //console.log(senderData);
    //console.log(recipientData);

    const onSubmit = () => {
        let dataForOrder = {
            offerId: acceptedOffer.offerId,
            senderName: senderData.name, senderSurname: senderData.surname, senderEmail: senderData.email, senderPhone: senderData.phone,
            receiverName: recipientData.name, receiverSurname: recipientData.surname, receiverEmail: recipientData.email, receiverPhone: recipientData.phone
        };
        const res = fetch('/api2/order', {
            method: 'POST',
            body: JSON.stringify(dataForOrder),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Something went wrong ...');
                }
            })

        navigate("/order-placed-page");
    };


    return (
        <>
            <div className="card p-3">
                <div className="card-header mb-3">
                    <h4>Summary</h4>
                </div>

                <div className="row">
                    <h5>Source address: {inquiryData.sourceAddress.country + " " + inquiryData.sourceAddress.city + " " + inquiryData.sourceAddress.street}</h5>
                    <h5>Destination address: {inquiryData.destinationAddress.country + " " + inquiryData.destinationAddress.city + " " + inquiryData.destinationAddress.street}</h5>
                    <h5>Price: {acceptedOffer.fullPrice.amount.toFixed(2)} {acceptedOffer.fullPrice.currency}</h5>
                    {
                        acceptedOffer.priceBreakdown.map((price, index) => { return <h5 key={index}>{price.description}: {price.amount} {price.currency}</h5> }
                        )
                    }
                </div>
            </div>
            <div className="container-forms-buttons">
                <button className="btn btn-danger" id="back-button" onClick={() => navigate(-1)}>Go back</button>
                <button className="btn btn-primary" id="next-button" onClick={onSubmit}>Submit</button>
            </div>
        </>
    );
}

export default InquirySummary;