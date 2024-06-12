import { AxiosResponse } from "axios";
import { BlogPostDto } from "../types/blogPost";
import { api } from "./api";
import { CreateBlogPostFormInput, UpdateBlogPostFormInput } from "@/features/blogPosts/abstract";

const blogPostApi = {
    getLatestBlogPosts: async (): Promise<BlogPostDto[]> =>
    {
        const response = await api.get<BlogPostDto[]>( `/blogposts/latest` );

        return response.data;
    },

    getBlogPostById: async ( id: number ): Promise<BlogPostDto> =>
    {
        const response = await api.get<BlogPostDto>( `/blogposts/${ id }` );
        return response.data;
    },

    getBlogPostByAuthorId: async ( authorId: number ): Promise<BlogPostDto[]> =>
    {
        const response = await api.get<BlogPostDto[]>( `/blogposts/author/${ authorId }` );
        return response.data;
    },

    getLatestBlogPostByAuthorId: async ( authorId: number ): Promise<BlogPostDto[]> =>
    {
        const response = await api.get<BlogPostDto[]>( `/blogposts/latest/${ authorId }` );
        return response.data;
    },

    getBlogPostsCount: async () =>
    {
        const response = await api.get<number>( '/blogposts/count' );
        return response.data;
    },

    createBlogPost: async ( data: CreateBlogPostFormInput | FormData ): Promise<BlogPostDto> =>
    {
        const response: AxiosResponse<BlogPostDto> = await api.post( "blogposts/create", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );

        return response.data;
    },

    updateBlogPost: async ( data: UpdateBlogPostFormInput ): Promise<string> =>
    {
        const response = await api.patch( "blogposts/update", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );

        return response.data.id;
    },

    deleteBlogPost: async ( data: BlogPostDto ): Promise<void> =>
    {
        const response = await api.delete( "blogposts/delete", {
            headers: { 'Content-Type': 'application/json' },
            data: data,
        } );
        
        return response.data;
    }

};

export default blogPostApi;