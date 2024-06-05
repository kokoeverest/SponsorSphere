import * as yup from "yup";

const uploadPictureSchema = yup.object().shape( {
    formFile: yup.mixed<File>().required( "A file is required" ),
} );

export { uploadPictureSchema };
