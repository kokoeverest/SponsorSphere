import * as yup from "yup";

const uploadPictureSchema = yup.object().shape( {
    formFile: yup.mixed<File>().required( "A file is required" ),
} );

const pictureSchema = yup.object().shape({
    id: yup.number().min(1).required(),
    url: yup.string().optional(),
    content: yup.mixed(),
    modified: yup.date().nullable(),
})

export { uploadPictureSchema, pictureSchema };
