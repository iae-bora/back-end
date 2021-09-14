using IaeBoraLibrary.Model;

namespace IaeBoraLibrary.Service
{
    public static class RouteService
    {
        public static Route CreateDetailedRoute(Answer answer)
        {
            var routeCategories = IaeBoraMLService.GetRouteCategories(answer);

            // TODO:
            // Enviar para o serviço de criação da rota detalhada
            // retornar a rota com o objeto detalhado instanciado no objeto.
            // Salvar no banco, essa rota.

            return null;
        }
    }
}
