
export const weight_validation = {
    name: 'weight',
    label: 'Weight',
    type: 'number',
    id: 'weight',
    placeholder: 'write weight ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        min: {
            value: 1,
            message: 'value must be greater than 0',
        }
    },
}

export const width_validation = {
    name: 'width',
    label: 'Width',
    type: 'number',
    id: 'width',
    placeholder: 'write width ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        min: {
            value: 1,
            message: 'value must be greater than 0!',
        }
    },
}

export const length_validation = {
    name: 'length',
    label: 'Length',
    type: 'number',
    id: 'length',
    placeholder: 'write length ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        min: {
            value: 1,
            message: 'value must be greater than 0!',
        }
    },
}

export const height_validation = {
    name: 'height',
    label: 'Height',
    type: 'number',
    id: 'height',
    placeholder: 'write height ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        min: {
            value: 1,
            message: 'value must be greater than 0!',
        }
    },
}

export const sourceAddress_validation = {
    name: 'sourceAddress',
    label: 'Source Address',
    type: 'text',
    id: 'sourceAddress',
    placeholder: 'write source address ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const sourceCountry_validation = {
    name: 'sourceCountry',
    label: 'country',
    type: 'text',
    id: 'sourceCountry',
    placeholder: 'write your country ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const sourceHouseNumber_validation = {
    name: 'sourceHouseNumber',
    label: 'house number',
    type: 'text',
    id: 'sourceHouseNumber',
    placeholder: 'write your house number ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const sourceApartmentNumber_validation = {
    name: 'sourceApartmentNumber',
    label: 'apartment number',
    type: 'text',
    id: 'sourceApartmentNumber',
    placeholder: 'write your apartment number ...',
    validation: {
    },
}

export const sourceStreet_validation = {
    name: 'sourceStreet',
    label: 'street',
    type: 'text',
    id: 'sourceStreet',
    placeholder: 'write your street ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const sourceCity_validation = {
    name: 'sourceCity',
    label: 'city',
    type: 'text',
    id: 'sourceCity',
    placeholder: 'write your city ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const sourceZipCode_validation = {
    name: 'sourceZipCode',
    label: 'zip code',
    type: 'text',
    id: 'sourceZipCode',
    placeholder: 'write your zip code ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const destinationCountry_validation = {
    name: 'destinationCountry',
    label: 'country',
    type: 'text',
    id: 'destinationCountry',
    placeholder: 'write your country ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const destinationHouseNumber_validation = {
    name: 'destinationHouseNumber',
    label: 'house number',
    type: 'text',
    id: 'destinationHouseNumber',
    placeholder: 'write your house number ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const destinationApartmentNumber_validation = {
    name: 'destinationApartmentNumber',
    label: 'apartment number',
    type: 'text',
    id: 'destinationApartmentNumber',
    placeholder: 'write your apartment number ...',
    validation: {
    },
}

export const destinationStreet_validation = {
    name: 'destinationStreet',
    label: 'street',
    type: 'text',
    id: 'destinationStreet',
    placeholder: 'write your street ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const destinationCity_validation = {
    name: 'destinationCity',
    label: 'city',
    type: 'text',
    id: 'destinationCity',
    placeholder: 'write your city ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const destinationZipCode_validation = {
    name: 'destinationZipCode',
    label: 'zip code',
    type: 'text',
    id: 'destinationZipCode',
    placeholder: 'write your zip code ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const destinationAddress_validation = {
    name: 'destinationAddress',
    label: 'Destination Address',
    type: 'text',
    id: 'destinationAddress',
    placeholder: 'write destination address ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

export const pickupDate_validation = {
    name: 'pickupDate',
    label: 'Pickup Date',
    type: 'date',
    id: 'pickupDate',
    validation: {
        required: {
            value: true,
            message: 'required',            
        },
        min: {
            value: new Date().toISOString().split('T')[0],
            message: 'Date cannot be in the past!',
        },
    },
}

export const deliveryDate_validation = {
    name: 'deliveryDate',
    label: 'Delivery Date',
    type: 'date',
    id: 'deliveryDate',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        min: {
            value: new Date().toISOString().split('T')[0],
            message: 'Date cannot be in the past!',
        },
    },
}

export const isCompany_validation = {
    name: 'isCompany',
    label: 'Is Company',
    type: 'checkbox',
    id: 'isCompany',
}

export const isPriorityHigh_validation = {
    name: 'isPriorityHigh',
    label: 'Is Priority High',
    type: 'checkbox',
    id: 'isPriorityHigh',
}

export const deliveryAtWeekend_validation = {
    name: 'deliveryAtWeekend',
    label: 'Delivery At Weekend',
    type: 'checkbox',
    id: 'deliveryAtWeekend',
}

export const name_validation = {
    name: 'name',
    label: 'name',
    type: 'text',
    id: 'name',
    placeholder: 'write your name ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        maxLength: {
            value: 30,
            message: '30 characters max',
        },
    },
}

export const surname_validation = {
    name: 'surname',
    label: 'surname',
    type: 'text',
    id: 'surname',
    placeholder: 'write your surname ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        maxLength: {
            value: 30,
            message: '30 characters max',
        },
    },
}

export const phone_validation = {
    name: 'phone',
    label: 'phone',
    type: 'tel',
    id: 'phone',
    placeholder: 'write your phone ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        pattern: {
            value:
                /^\d{9}$/,
            message: 'phone not valid',
        },
    },
}

export const email_validation = {
    name: 'email',
    label: 'email',
    type: 'email',
    id: 'email',
    placeholder: 'write your email ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        pattern: {
            value:
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
            message: 'e-mail not valid',
        },
    },
}

export const desc_validation = {
    name: 'description',
    label: 'description',
    multiline: true,
    id: 'description',
    placeholder: 'write description ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        maxLength: {
            value: 200,
            message: '200 characters max',
        },
    },
}

export const password_validation = {
    name: 'password',
    label: 'password',
    type: 'password',
    id: 'password',
    placeholder: 'type password ...',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
        minLength: {
            value: 6,
            message: 'min 6 characters',
        },
    },
}

export const num_validation = {
    name: 'num',
    label: 'number',
    type: 'number',
    id: 'num',
    placeholder: 'write a random number',
    validation: {
        required: {
            value: true,
            message: 'required',
        },
    },
}

//export const email_validation = {
//    name: 'email',
//    label: 'email address',
//    type: 'email',
//    id: 'email',
//    placeholder: 'write a random email address',
//    validation: {
//        required: {
//            value: true,
//            message: 'required',
//        },
//        pattern: {
//            value:
//                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
//            message: 'not valid',
//        },
//    },
//}
