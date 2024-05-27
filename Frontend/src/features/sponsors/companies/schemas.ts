import * as yup from 'yup';

export const RegisterCompanySchema = yup.object().shape({
    name: yup.string().min(2).max(200).required('First name is required'),
    iban: yup.string().min(15).max(34).required('Valid IBAN is required'),
    email: yup.string().email('Must be a valid email').required('Email is required'),
    password: yup.string().min(8).max(32).required('Password is required'),
    phoneNumber: yup.string().required('Phone number is required'),
    country: yup.string().required('Country is required'),
});