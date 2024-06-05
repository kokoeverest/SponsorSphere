import * as yup from "yup";
import { uploadPictureSchema } from "../pictures/schemas";

const CreateBlogPostSchema = yup.object().shape( {
    content: yup.string().min(50).required("Content must be at least 50 symbols or more"),
    authorId: yup.number().required( 'Author id is required' ),
    pictures: yup.array().of(uploadPictureSchema).required("Upload at least one picture"),
} );

export default CreateBlogPostSchema;