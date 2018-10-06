function J = mydeconvwnr(I, PSF, NSR)
%   deconvolves image I using the Wiener filter algorithm, 
%   returning deblurred image J. Image I can be an N-dimensional array. 
%   PSF is the point-spread function with which I was convolved. 
%   NSR is the noise-to-signal power ratio of the additive noise. 
%   NSR can be a scalar or a spectral-domain array of the same size as I. 

% Compute H so that it has the same size as I.
% psf2otf computes the FFT of the PSF array
% and creates the OTF(optical transfer function) array. 
sizeI = size(I);
H = psf2otf(PSF, sizeI);

S_u = NSR;
S_x = 1;

% Compute the Wiener restoration filter:
%
%                   H*(k,l)
% G(k,l)  =  ------------------------------
%            |H(k,l)|^2 + S_u(k,l)/S_x(k,l)
%
% where S_x is the signal power spectrum and S_u is the noise power
% spectrum.
%
% To minimize issues associated with divisions, the equation form actually
% implemented here is this:
%
%                   H*(k,l) S_x(k,l)
% G(k,l)  =  ------------------------------
%            |H(k,l)|^2 S_x(k,l) + S_u(k,l)
%

% Compute the denominator of G in pieces.
denom = abs(H).^2;
denom = denom .* S_x;
denom = denom + S_u;
clear S_u

% Make sure that denominator is not 0 anywhere.  Note that denom at this
% point is nonnegative, so we can just add a small term without fearing a
% cancellation with a negative number in denom.
denom = max(denom, sqrt(eps));

G = conj(H) .* S_x;
clear H S_x
G = G ./ denom;
clear denom

% Apply the filter G in the frequency domain.
J = ifftn(G .* fftn(I));
clear G

J = real(J);