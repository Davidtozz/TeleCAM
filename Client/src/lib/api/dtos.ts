export type LoginDto = {
    username: string;
    password: string;
}

export type RegisterDto = LoginDto & {
    email: string;
}

export type TokenResponseDto = {
    accessToken: string;
    refreshToken: string;
}