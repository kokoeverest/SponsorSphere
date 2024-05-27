import * as yup from 'yup';

export const RegisterIndividualSchema = yup.object().shape({
    name: yup.string().min(2).max(200).required('First name is required'),
    lastName: yup.string().min(2).max(200).required('Last name is required'),
    email: yup.string().email('Must be a valid email').required('Email is required'),
    password: yup.string().min(8).max(32).required('Password is required'),
    birthDate: yup.string().required('Birthdate is required').typeError('Invalid date format'),
    phoneNumber: yup.string().required('Phone number is required'),
    country: yup.string().required('Country is required'),
});