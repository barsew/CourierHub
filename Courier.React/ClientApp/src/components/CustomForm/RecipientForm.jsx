import React from 'react';
import { Input } from './Input'
import { FormProvider, useForm } from 'react-hook-form'
import {
    surname_validation,
    phone_validation,
    email_validation,
    name_validation
} from './utils/inputValidations'
import { useNavigate, useLocation } from 'react-router-dom';

function RecipientForm() {

    const methods = useForm();
    const navigate = useNavigate();
    const location = useLocation();
    const acceptedOffer = location.state?.acceptedOffer;
    const inquiryData = location.state?.inquiry;
    const senderData = location.state?.senderData;

    const onSubmit = methods.handleSubmit(recipientData => {
        navigate("/inquiry-summary", { state: { acceptedOffer: acceptedOffer, inquiry: inquiryData, senderData: senderData, recipientData: recipientData } });    
    })


    return (
        <div className="card" >
            <div className="card-header">
                <h5>Recipient Form</h5>
            </div>
        <FormProvider {...methods}>
            <form
                onSubmit={e => e.preventDefault()}
                noValidate
                autoComplete="on"
                className="inquiryForm"
            >
                <div className="inquiry-form">
                    <div className="container">
                        <div className="row">
                            <div className="col">
                                <Input {...name_validation} />
                            </div>
                            <div className="col">
                                <Input {...surname_validation} />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col">
                                <Input {...phone_validation} />
                            </div>
                            <div className="col">
                                <Input {...email_validation} />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="mt-5">
                        <div className="container-forms-buttons">
                            <button className="btn btn-danger" id="back-button" onClick={() => navigate(-1)}>Go back</button>
                            <button className="btn btn-primary" id="next-button" onClick={onSubmit}>Next</button>
                        </div>
                </div>
            </form>
            </FormProvider>
        </div>
    )
}

export default RecipientForm;
