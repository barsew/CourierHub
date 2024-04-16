import React, { useMemo } from 'react';
import { Input } from './Input'
import { FormProvider, useForm } from 'react-hook-form'
import {
    surname_validation,
    phone_validation,
    name_validation
} from './utils/inputValidations'
import { useNavigate, useLocation } from 'react-router-dom';

function UserDataForm() {

    const location = useLocation();
    const navigate = useNavigate();
    const auth0Id = location.state?.auth0Id;
    const userName = location.state?.incompleteData.name;
    const userSurname = location.state?.incompleteData.surname;
    const userPhone = location.state?.incompleteData.phone;

    const initialData = {
        name: userName || '',
        surname: userSurname || '',
        phone: userPhone || '',
    };

    const methods = useForm({
        defaultValues: useMemo(() => {
            return initialData;
        }, [initialData])
    });

    const onSubmit = methods.handleSubmit(userData => {
        const data = { name: userData.name, surname: userData.surname, phone: userData.phone, auth0Id: auth0Id }
        fetch('/api2/completedata', {
            method: 'POST',
            body: JSON.stringify(data),
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
        navigate("/");
    })


    return (
        <div className="card" >
            <div className="card-header">
                <h5>Complete your data</h5>
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
                                    <Input {...name_validation} defaultValue={initialData.name} />
                                </div>
                                <div className="col">
                                    <Input {...surname_validation} />
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <Input {...phone_validation} />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="mt-5">
                        <div className="container-forms-buttons">
                            <button className="btn btn-primary" id="next-button" onClick={onSubmit}>Save data</button>
                        </div>
                    </div>
                </form>
            </FormProvider>
        </div>
    )
}

export default UserDataForm;
