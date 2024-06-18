import SvgIcon from '@mui/material/SvgIcon';
import { SvgIconProps } from '@mui/material/SvgIcon';

const StravaIcon = ( props: SvgIconProps ) => (
    <SvgIcon { ...props }>
        <svg
            viewBox="0 0 48 48"
            xmlns="http://www.w3.org/2000/svg">
            <g
                fill="none"
                stroke="#000"
                strokeLinecap="round"
                strokeLinejoin="round">
                <path
                    d="m31.016 26.855-11.177-22.355-11.178 22.355" />
                <path
                    d="m22.694 26.855 8.322 16.645 8.323-16.645" />
            </g>
        </svg>
    </SvgIcon>
);

export default StravaIcon;