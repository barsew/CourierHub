import { findInputError } from './utils/findInputError'
import { isFormInvalid } from './utils/isFormInvalid';
import { useFormContext } from 'react-hook-form'
import { AnimatePresence, motion } from 'framer-motion'
import { MdError } from 'react-icons/md'

export const Input = ({ label, type, id, placeholder, validation, name }) => {
    const {
        register,
        formState: { errors },
    } = useFormContext()

    const inputError = findInputError(errors, id)
    const isInvalid = isFormInvalid(inputError)

    return (
        <div className="input-form">
            <div className="row">
                <div className="col mb-3 w-30" style={{width: '50px'}}>
                    <label htmlFor={id} className="input-label">
                        {label}
                    </label>
                </div>
                <div className="col" style={{ width: '50px' }}>
                    <AnimatePresence mode="wait" initial={false}>
                        {isInvalid && (
                            <InputError
                                message={inputError.error.message}
                                key={inputError.error.message}
                            />
                        )}
                    </AnimatePresence>
                </div>
            </div>
            <input
                id={id}
                type={type}
                placeholder={placeholder}
                {...register(name, validation)}
            />
        </div>
    )
}
const InputError = ({ message }) => {
    return (
        <motion.p
            style={{color: 'red'}}
            {...framer_error}
        >
            <MdError />
            {message}
        </motion.p>
    )
}

const framer_error = {
    initial: { opacity: 0, y: 10 },
    animate: { opacity: 1, y: 0 },
    exit: { opacity: 0, y: 10 },
    transition: { duration: 0.2 },
}
