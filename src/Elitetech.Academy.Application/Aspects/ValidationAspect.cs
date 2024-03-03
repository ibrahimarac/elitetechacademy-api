using ArxOne.MrAdvice.Advice;
using Elitetech.Academy.Application.Wrapper;
using FluentValidation;
using FluentValidation.Results;

namespace Elitetech.Academy.Application.Aspects
{
    public class ValidationAspect : Attribute, IMethodAdvice
    {
        private readonly Type _validatorType;
        private readonly bool _isAsync;

        public ValidationAspect(Type validatorType, bool isAsync)
        {
            _validatorType = validatorType;
            _isAsync = isAsync;
        }

        public void Advise(MethodAdviceContext context)
        {
            var methodParameters = context.Arguments;
            if (!methodParameters.Any())
                throw new InvalidOperationException("Parametre almayan bir metod için validasyon kullanılamaz.");

            //AbstractValidator<T> türünü getirir.
            var abstractValidatorType = typeof(AbstractValidator<>);

            //Metoda gelen ve validate edilecek model türünü getirir.
            var parameterType = methodParameters.First().GetType();

            //AbstractValidator<AnnouncementCreateRequestDto> türünü getirir.
            var modelValidatorType = abstractValidatorType.MakeGenericType(parameterType);

            if (_validatorType.BaseType != modelValidatorType)
                throw new InvalidOperationException("Gönderilen tür bu model için yazılmış bir validasyon içermiyor.");

            var ci = _validatorType.GetConstructor(Type.EmptyTypes);
            var concrateValidator = ci?.Invoke(null);
            var validateMethod = _validatorType.GetMethod("Validate", new Type[] { parameterType });

            if(validateMethod is null)
            {
                throw new InvalidOperationException("İlgili validasyon sınıfı için Validate metodu bulunamadı.");
            }                
            else if(validateMethod.Invoke(concrateValidator, new[] { context.Arguments.First() }) is ValidationResult validationResult)
            {
                if (!validationResult.IsValid)
                {
                    var result = Result.Error(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

                    if (_isAsync)
                        context.ReturnValue = Task.FromResult(result);
                    else
                        context.ReturnValue = result;
                }
                else
                {
                    context.Proceed();
                }
            }
            
        }
    }
}
