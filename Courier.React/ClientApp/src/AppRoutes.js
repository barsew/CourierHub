import Home from "./components/Home";
import Inquiries from "./components/Inquiry/Inquiries";
import Offers from "./components/Offer/Offers"
import Profile from "./components/Login/Profile";
import OrderPlacedPage from "./components/Order/OrderPlacedPage";
import InquiryForm from "./components/CustomForm/InquiryForm";
import SenderForm from "./components/CustomForm/SenderForm";
import RecipientForm from "./components/CustomForm/RecipientForm";
import InquirySummary from "./components/Inquiry/InquirySummary";
import LoginPage from "./components/Login/LoginPage";
import Orders from "./components/Order/Orders";
import OrderDetails from "./components/Order/OrderDetails";
import OrderTracking from "./components/Order/OrderTracking";
import UserDataForm from "./components/CustomForm/UserDataForm";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/inquiries-data',
        element: <Inquiries />
    },
    {
        path: '/orders-data',
        element: <Orders />
    },
    {
        path: '/orders-data/details/:id',
        element: <OrderDetails />
    },
    {
        path: '/tracking/:id',
        element: <OrderTracking />
    },
    {
        path: '/create-inquiry',
        element: <InquiryForm />
    },
    {
        path: '/offers',
        element: <Offers />
    },
    {
        path: '/profile',
        element: <Profile />
    },
    {
        path: '/order-placed-page',
        element: <OrderPlacedPage />
    },
    {
        path: '/sender-form',
        element: <SenderForm />
    },
    {
        path: '/recipient-form',
        element: <RecipientForm />
    },
    {
        path: '/inquiry-summary',
        element: <InquirySummary />
    },
    {
        path: '/login-page',
        element: <LoginPage />
    },
    {
        path: '/userdata-form',
        element: <UserDataForm />
    },
];

export default AppRoutes;
