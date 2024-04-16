import React, { useEffect, useState, useMemo } from 'react';
import { Input } from './Input'
import { FormProvider, useForm } from 'react-hook-form'
import {
    surname_validation,
    phone_validation,
    email_validation,
    name_validation
} from './utils/inputValidations'
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth0 } from "@auth0/auth0-react";
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';


function SenderForm() {

    const [userSenderData, setUserSenderData] = useState({name: "", surname: "", phone: "", email: ""});
    const { user, isAuthenticated } = useAuth0();
    const methods = useForm({
        defaultValues: useMemo(() => {
            return userSenderData;
        }, [userSenderData])
});
    const navigate = useNavigate();
    const location = useLocation()
    const acceptedOffer = location.state?.acceptedOffer;
    const inquiryData = location.state?.inquiry;

    const onSubmit = methods.handleSubmit(senderData => {
        navigate("/recipient-form", { state: { acceptedOffer: acceptedOffer, inquiry: inquiryData, senderData: senderData } });    
    })

    const getUserData = (isChecked) => {
        if (isChecked) {

            const fetchSenderUserData = async () => {
                fetch(`/api2/completedata`, {
                    headers: {
                        'Content-Type': 'application/json',
                    }
                })
                    .then((result) => result.json())
                    .then(data => setUserSenderData(data));
            }
            fetchSenderUserData();
        }
    }

    useEffect(() => {
        methods.reset(userSenderData);
    }, [userSenderData]);

    return (
        <>
            <div className="card" >
                <div className="card-header">
                    <h5>Sender Form</h5>
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
                        <div>
                            {isAuthenticated &&
                                <FormControlLabel
                                    control={<Checkbox
                                        value="sender-data"
                                        color="primary"
                                        onClick={(event) => getUserData(event.target.checked)} />}
                                    label="Use your account data"

                                />
                            }
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
        </>
    )
}

export default SenderForm;
