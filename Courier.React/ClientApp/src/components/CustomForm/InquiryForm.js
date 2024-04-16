import React, { useState } from 'react';
import { Input } from './Input'
import { FormProvider, useForm } from 'react-hook-form'
import { Box, styled } from '@mui/material';

import {
    weight_validation,
    width_validation,length_validation,
    height_validation,sourceAddress_validation,
    destinationAddress_validation,pickupDate_validation,
    deliveryDate_validation,isCompany_validation,
    isPriorityHigh_validation,deliveryAtWeekend_validation,
    sourceCountry_validation,sourceCity_validation,
    sourceZipCode_validation, sourceStreet_validation,
    sourceHouseNumber_validation,sourceApartmentNumber_validation, 
    destinationCountry_validation,destinationCity_validation,
    destinationZipCode_validation,destinationStreet_validation,
    destinationHouseNumber_validation, destinationApartmentNumber_validation, 
} from './utils/inputValidations'
import { useNavigate } from 'react-router-dom';

const StyledBox = styled(Box)({
    background: 'linear-gradient(#755139, #feedca)',
});

function InquiryForm() {

    const methods = useForm();
    const navigate = useNavigate();
    const [errorPickupDeliveryDate, setErrorPickupDeliveryDate] = useState(false);

    async function postInquiry(inquiryData) {

            const response = await fetch('/api2/inquiry', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    ...inquiryData,
                    sourceAddress:
                    {
                        houseNumber: inquiryData.sourceHouseNumber, apartmentNumber: inquiryData.sourceApartmentNumber,
                        street: inquiryData.sourceStreet, city: inquiryData.sourceCity, zipCode: inquiryData.sourceZipCode,
                        country: inquiryData.sourceCountry
                    },
                    destinationAddress:
                    {
                        houseNumber: inquiryData.destinationHouseNumber, apartmentNumber: inquiryData.destinationApartmentNumber,
                        street: inquiryData.destinationStreet, city: inquiryData.destinationCity, zipCode: inquiryData.destinationZipCode,
                        country: inquiryData.destinationCountry
                    }
                }),
            });

        if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            const responseData = await response.json();
        navigate("/offers", {
            state: {
                offers: responseData, inquiry: {
                    ...inquiryData,
                    sourceAddress:
                    {
                        houseNumber: inquiryData.sourceHouseNumber, apartmentNumber: inquiryData.sourceApartmentNumber,
                        street: inquiryData.sourceStreet, city: inquiryData.sourceCity, zipCode: inquiryData.sourceZipCode,
                        country: inquiryData.sourceCountry
                    },
                    destinationAddress:
                    {
                        houseNumber: inquiryData.destinationHouseNumber, apartmentNumber: inquiryData.destinationApartmentNumber,
                        street: inquiryData.destinationStreet, city: inquiryData.destinationCity, zipCode: inquiryData.destinationZipCode,
                        country: inquiryData.destinationCountry
                    }
                } } });    
    };

    const onSubmit = methods.handleSubmit(data => {
        if (data?.pickupDate > data?.deliveryDate) {
            setErrorPickupDeliveryDate(true);
        }
        else {
            postInquiry(data);
            setErrorPickupDeliveryDate(false);
        }
   
    })

    return (
        <StyledBox>
            <>
            {errorPickupDeliveryDate && <div className="errorMessage">Pickup date cannot be greater than delivery date!</div>}
            <FormProvider {...methods}>
                <form
                    onSubmit={e => e.preventDefault()}
                    noValidate
                    autoComplete="off"
                    className="inquiryForm"
                >
                    <div className="inquiry-form">
                            <div className="container">

                                <div className="card mb-3">
                                    <div className="card-header">
                                        <h5>Dimensions</h5>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...weight_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...width_validation} />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...length_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...height_validation} />
                                        </div>
                                    </div>
                                </div>
                            
                                <div className="card mb-3">
                                    <div className="card-header"><h5>Source Address</h5></div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...sourceCountry_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...sourceCity_validation} />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...sourceZipCode_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...sourceStreet_validation} />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...sourceHouseNumber_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...sourceApartmentNumber_validation} />
                                        </div>
                                </div>


                                </div>
                                <div className="card mb-3">
                                    <div className="card-header"><h5>Destination Address</h5></div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...destinationCountry_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...destinationCity_validation} />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...destinationZipCode_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...destinationStreet_validation} />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...destinationHouseNumber_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...destinationApartmentNumber_validation} />
                                        </div>
                                    </div>
                                </div>

                                <div className="card mb-3">
                                    <div className="card-header">
                                        <h5>
                                            Dates
                                        </h5>
                                    </div>
                                    <div className="row">
                                        <div className="col">
                                            <Input {...pickupDate_validation} />
                                        </div>
                                        <div className="col">
                                            <Input {...deliveryDate_validation} />
                                        </div>
                                    </div>
                                </div>

                            
                            
                            <div className="row">
                                <div className="col">
                                    <Input {...isCompany_validation} />
                                </div>
                                <div className="col">
                                    <Input {...isPriorityHigh_validation} />
                                </div>
                                <div className="col" >
                                    <Input {...deliveryAtWeekend_validation} />
                                </div>
                            </div>
                        </div>
                    </div>
                        <div className="mt-5">
                            <div className="container-forms-buttons">
                                <button className="btn btn-danger" disabled={true} id="back-button">Go back</button>
                                <button className="btn btn-primary" id="next-button" onClick={onSubmit}>Next</button>
                            </div>
                    </div>
                </form>
                </FormProvider>
            </>
        </StyledBox>
    )
}

export default InquiryForm;
