import apiClient, { ApiResponse, PagedResponse } from './apiClient';

export interface ConferenceDto {
    conferenceId: string; // Backend dùng GUID
    name: string;
    acronym: string;
    description?: string;
    location?: string;
    startDate: string;
    endDate: string;
    status: string;
}

export interface CreateConferenceRequest {
    name: string;
    acronym: string;
    description?: string;
    location?: string;
    startDate?: string;
    endDate?: string;
}

const conferenceApi = {
    getAll: async (status?: string, page: number = 1, pageSize: number = 10) => {
        const params = new URLSearchParams();
        if (status) params.append('status', status);
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        const response = await apiClient.get<ApiResponse<PagedResponse<ConferenceDto>>>(`/api/conferences?${params}`);
        return response.data;
    },

    create: async (data: CreateConferenceRequest) => {
        const response = await apiClient.post<ApiResponse<ConferenceDto>>('/api/conferences', data);
        return response.data;
    }
};

export default conferenceApi;