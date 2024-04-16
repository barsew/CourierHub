import React from 'react';
import { Link } from 'react-router-dom';

function OrderPlacedPage() {

    return (
        <div className="card text-white bg-success mb-3">
            <div className="card-header">Success</div>
            <div className="card-body">
                <h5 className="card-title">Your order has been placed.</h5>
                <p className="card-text">Thank you for your order.</p>
                <Link to="/">Go back to home page</Link>
            </div>
        </div>
    );
}

export default OrderPlacedPage;