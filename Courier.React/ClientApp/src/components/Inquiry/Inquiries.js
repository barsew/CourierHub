import React, { useState, useEffect } from 'react'
import { InquiriesTable } from '../CustomTable/InquiriesTable';
import { useAuth0 } from "@auth0/auth0-react";

const Inquiries = () =>
{
    const { isAuthenticated } = useAuth0();

    return (
        <>
            {!isAuthenticated ?
                <h5>You must be logged in to see your inquiries</h5> :
                <div style={{ textAlign: 'center', color: 'black' }}>
                    <h1>Inquiries List</h1>
                    <InquiriesTable />
                </div>
            }
        </>
    )
}

export default Inquiries;
